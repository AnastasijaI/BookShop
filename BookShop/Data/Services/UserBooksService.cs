using BookShop.Areas.Identity.Data;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookShop.Data.Services
{
    public class UserBooksService : IUserBooksService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<BookShopUser> _userManager;

        public UserBooksService(AppDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<BookShopUser> userManager)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        //public async Task<bool> DeleteAsync(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return false; 
        //    }
        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();
        //    return true; 
        //}
        public async Task<string> GetCurrentUserId()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return user?.Id;
        }
        public async Task<bool> BuyBook(int bookId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return false; // User not authenticated
            }

            var userBook = new UserBook
            {
                AppUser = userId,
                BookId = bookId
            };

            _context.UserBooks.Add(userBook);
            await _context.SaveChangesAsync();
            return true;
        }

        public string GetCurrentUserUsername()
        {
            var curUser = GetCurrentUserId().Result; // Await the task to get the actual result
            var username = _context.Users.FirstOrDefault(i => i.Id == curUser)?.UserName;
            return username;
        }
        public async Task<List<Book>> GetAllUserBooks()
        {
            var curUser = await GetCurrentUserId(); // Await the task to get the actual user ID
            List<UserBook> userBooks = await _context.UserBooks.Where(u => u.AppUser == curUser).ToListAsync();
            List<Book> myBooks = new List<Book>();
            foreach (var book in userBooks)
            {
                var myBook = await _context.Books
                    .Include(a => a.Author)
                    .Include(r => r.Reviews)
                    .Include(bg => bg.BookGenres).ThenInclude(g => g.Genre)
                    .FirstOrDefaultAsync(i => i.Id == book.BookId);
                myBooks.Add(myBook);
            }
            return myBooks;
        }


        public async Task<bool> HasBook(int bookId) 
        {
            var curUser = await GetCurrentUserId(); 
            var hasBook = await _context.UserBooks
                .FirstOrDefaultAsync(u => u.AppUser == curUser && u.BookId == bookId);
            return hasBook != null; 
        }


        public async Task<BookShopUser> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }


        public bool Add(UserBook userBooks)
        {
            _context.UserBooks.Add(userBooks);
            return Save();
        }

        public bool Update(UserBook userBooks)
        {
            _context.UserBooks.Update(userBooks);
            return Save();
        }

        public bool Delete(UserBook userBooks)
        {
            _context.UserBooks.Remove(userBooks);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<bool> AddBookToUser(string userId, int bookId)
        {
            // Проверка дали корисникот постои
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false; // Корисникот не е пронајден
            }

            // Проверка дали книгата постои
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return false; // Книгата не е пронајдена
            }

            // Додавање на купената книга во базата на податоци
            var userBook = new UserBook
            {
                AppUser = userId,
                BookId = bookId
            };

            _context.UserBooks.Add(userBook);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteBookFromUser(string userId, int bookId)
        {
            // Proveri dali korisnikot go ima knigata
            var userBook = await _context.UserBooks.FirstOrDefaultAsync(ub => ub.AppUser == userId && ub.BookId == bookId);
            if (userBook == null)
            {
                return false; // Korisnikot nema ovaa kniga
            }

            // Izbrisi ja knigata samo od listata na korisnikot
            _context.UserBooks.Remove(userBook);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

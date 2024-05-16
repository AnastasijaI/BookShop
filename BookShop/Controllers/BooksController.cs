using BookShop.Data.Services;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.viewModel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using BookShop.Areas.Identity.Data;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
       // private readonly AppDbContext _context;
        private readonly IBooksService _service;
        private readonly IAuthorsService _authorsService;
        private readonly IGenresService _genresService;
        private readonly IBooksGenresService _bookGenresService;
        //private readonly UserManager<BookShopUser> _userManager;

        public BooksController(IBooksService service, IAuthorsService authorsService, IGenresService genresService, IBooksGenresService bookGenresService/*, UserManager<BookShopUser> userManager, AppDbContext context*/)
        {
            _service = service;
            _authorsService = authorsService;
            _genresService = genresService;
            _bookGenresService = bookGenresService;
            //_context = context;
        }
        public async Task<IActionResult> Index(string searchString1, string searchString2, string searchString3)
        {
            var allBooks = await _service.GetAllAsync();
            if (!String.IsNullOrEmpty(searchString1))
            {
                allBooks = allBooks.Where(n => n.Title.Contains(searchString1)).ToList();
            }
            if (!String.IsNullOrEmpty(searchString2))
            {
                allBooks = allBooks.Where(n => n.BookGenres.Any(
                        bg => bg.Genre.GenreName.Contains(searchString2))
                );
            }
            if (!String.IsNullOrEmpty(searchString3))
            {
                allBooks = allBooks.Where(n => n.Author.FirstName.Contains(searchString3) || n.Author.LastName.Contains(searchString3)).ToList();
            }
            return View(allBooks);
        }
        public async Task<IActionResult> SearchByAuthorId(int id)
        {
            IEnumerable<Book> books = await _service.GetBooksByAuthorId(id);
            return View(books);
        }
        //Get: Books/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Author> authors = await _authorsService.GetAllAsync();
            IEnumerable<Genre> genres = await _genresService.GetAllAsync();
            CreateBooksViewModel newVM = new CreateBooksViewModel()
            {
                Authors = authors,
                Genres = genres,
            };
            return View(newVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBooksViewModel bookVM, IFormFile frontPageImage)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Author> authors = await _authorsService.GetAllAsync();
                IEnumerable<Genre> genres = await _genresService.GetAllAsync();
                bookVM.Authors = authors;
                bookVM.Genres = genres;
                ModelState.AddModelError("", "Failed to add book");
                return View(bookVM);
            }
            var newBook = new Book()
            {
                Title = bookVM.Title,
                YearPublished = bookVM.YearPublished,
                NumPages = bookVM.NumPages,
                Description = bookVM.Description,
                Publisher = bookVM.Publisher,
                FrontPage = bookVM.FrontPage,
                AuthorId = bookVM.AuthorId,
                DownloadUrl = bookVM.DownloadUrl,
            };
            _service.Add(newBook);

            if (frontPageImage != null && frontPageImage.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(frontPageImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await frontPageImage.CopyToAsync(stream);
                }

                newBook.FrontPage = "/images/" + fileName; 
            }

            Book newLastBook = await _service.GetLastBook();
            foreach (var genreId in bookVM.GenreIds)
            {
                BookGenre bookGenre = new BookGenre()
                {
                    BookId = newLastBook.Id,
                    GenreId = genreId,
                };
                _bookGenresService.Add(bookGenre);
            }
            return RedirectToAction("Index");
        }
        //Get: Book/Delete
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Book book = await _service.GetByIdAsync(id);
            if (book == null)
            {
                return View("NotFound");
            }
            return View(book);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBookConfirmed(int id)
        {
            Book book = await _service.GetByIdAsync(id);
            if (book == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        //Get: Books/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await _service.GetByIdAsyncNoTracking(id);
            if (book == null)
            {
                return View("NotFound");
            }
            IEnumerable<Author> authors = await _service.GetAllAuthors();
            IEnumerable<Genre> genres = await _service.GetAllGenres();
            List<int> genreIds = new List<int>();
            foreach (var genre in book.BookGenres)
            {
                genreIds.Add(genre.GenreId);
            }
            EditBookViewModel bookVM = new EditBookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                YearPublished = book.YearPublished,
                NumPages = book.NumPages,
                Publisher = book.Publisher,
                FrontPage = book.FrontPage,
                DownloadUrl = book.DownloadUrl,
                Authors = authors,
                Genres = genres,
                AuthorId = book.AuthorId,
                GenreIds = genreIds,
            };
            return View(bookVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookViewModel bookVM, IFormFile frontPageImage)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<Author> authors = await _service.GetAllAuthors();
                bookVM.Authors = authors;
                IEnumerable<Genre> genres = await _service.GetAllGenres();
                bookVM.Genres = genres;
                ModelState.AddModelError("", "Failed to edit book");
                return View(bookVM);
            }
            if (bookVM != null)
            {
                Book newBook = new Book()
                {
                    Id = bookVM.Id,
                    Title = bookVM.Title,
                    Description = bookVM.Description,
                    YearPublished = bookVM.YearPublished,
                    NumPages = bookVM.NumPages,
                    Publisher = bookVM.Publisher,
                    FrontPage = bookVM.FrontPage,
                    DownloadUrl = bookVM.DownloadUrl,
                    AuthorId = bookVM.AuthorId,
                };
                _service.Update(id, newBook);
                if (frontPageImage != null && frontPageImage.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(frontPageImage.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await frontPageImage.CopyToAsync(stream);
                    }

                    newBook.FrontPage = "/images/" + fileName;
                }
                IEnumerable<BookGenre> bookGenres = await _bookGenresService.GetAll();
                foreach (var bg in bookGenres)
                {
                    if (bg.BookId == id)
                    {
                        _bookGenresService.Delete(bg);
                    }
                }
                foreach (var genreId in bookVM.GenreIds)
                {
                    BookGenre bookGenre = new BookGenre()
                    {
                        BookId = bookVM.Id,
                        GenreId = genreId,
                    };
                    _bookGenresService.Add(bookGenre);
                }

                return Redirect("/Books/Details/" + id);
            }
            else
            {
                IEnumerable<Author> authors = await _service.GetAllAuthors();
                bookVM.Authors = authors;
                IEnumerable<Genre> genres = await _service.GetAllGenres();
                bookVM.Genres = genres;
                return View(bookVM);
            }
        }

        public async Task<IActionResult> BooksByAuthor(int authorId)
        {
            var books = await _service.GetBooksByAuthorId(authorId);
            if (books == null || !books.Any())
            {
                return View("NotFound");
            }
            ViewBag.AuthorName = books.First().Author.FirstName + " " + books.First().Author.LastName;
            return View(books);
        }
    //    [HttpPost]
    //    [Authorize]
    //    public async Task<IActionResult> Buy(int bookId)
    //    {
    //        var user = await _userManager.GetUserAsync(User);
    //        var book = await _context.Books.FindAsync(bookId);
    //        if (user != null && book != null)
    //        {
    //            var userBook = new UserBook
    //            {
    //                AppUser = user.Id,
    //                BookId = bookId
    //            };
    //            _context.UserBooks.Add(userBook);
    //            await _context.SaveChangesAsync();
    //            return Json(new { success = true });
    //        }
    //        return Json(new { success = false });
    //    }

    //    public async Task<bool> HasUserBoughtBook(BookShopUser user, int bookId)
    //    {
    //        return await _context.UserBooks.AnyAsync(ub => ub.AppUser == user.Id && ub.BookId == bookId);
    //    }

    }
}


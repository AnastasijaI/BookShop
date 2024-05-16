using BookShop.Areas.Identity.Data;
using BookShop.Models;

namespace BookShop.Data.Services
{
    public interface IUserBooksService
    {
        Task<List<Book>> GetAllUserBooks();
        Task<string> GetCurrentUserId();
        Task<bool> BuyBook(int bookId);
        string GetCurrentUserUsername();
        Task<bool> HasBook(int bookId);
        Task<BookShopUser> GetUserById(int id);
        bool Add(UserBook userBooks);
        bool Update(UserBook userBooks);
        bool Delete(UserBook userBooks);
        bool Save();
        Task<bool> AddBookToUser(string userId, int bookId);
       // Task<bool> DeleteAsync(int id);
       Task<bool> DeleteBookFromUser(string userId, int bookId);
    }
}

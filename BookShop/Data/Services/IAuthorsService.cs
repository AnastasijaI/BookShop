using BookShop.Models;

namespace BookShop.Data.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(int id);
        Task AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(int id);
        Task<IEnumerable<Author>> SearchByNameOrLastNameAsync(string searchString);
    }
}

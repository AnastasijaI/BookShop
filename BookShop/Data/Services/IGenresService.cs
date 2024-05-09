using BookShop.Models;

namespace BookShop.Data.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre> GetByIdAsync(int id);
        Task AddAsync(Genre genre);
        Task<Genre> UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
    }
}

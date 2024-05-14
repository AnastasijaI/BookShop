using BookShop.Models;

namespace BookShop.Data.Services
{
    public interface IReviewsService
    {
        Task<IEnumerable<Review>> GetAllAsync(int id);
        Task<Review> GetByIdAsync(int id);
        Task AddAsync(Review review);
        Task<Review> UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}

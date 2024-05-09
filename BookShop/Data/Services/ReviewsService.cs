using BookShop.Data;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Services
{
    public class ReviewsService:IReviewsService
    {
        private readonly AppDbContext _context;
        public ReviewsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(n => n.Id == id);
            _context.Reviews.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetAllAsync(int id)
        {
            var result = await _context.Reviews.Where(b => b.BookId.Equals(id)).ToListAsync();
            return result;
        }

        public Task<IEnumerable<Review>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Review> UpdateAsync(Review newReview)
        {
            _context.Update(newReview);
            await _context.SaveChangesAsync();
            return newReview;
        }
    }
}

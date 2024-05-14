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

        //public async Task<IEnumerable<Review>> GetAllAsync()
        //{
        //    //Where(b => b.BookId.Equals(id))
        //    var result = await _context.Reviews.Include(b => b.Book).ToListAsync();
        //    return result;
        //}
        
        public async Task<Review> GetByIdAsync(int id)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Review> UpdateAsync(Review review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<IEnumerable<Review>> GetAllAsync(int id)
        {
            var result = await _context.Reviews.Include(b => b.Book).ToListAsync();
            return result;
        }
    }
}

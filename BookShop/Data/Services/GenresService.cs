using BookShop.Data;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Services
{
    public class GenresService : IGenresService
    {
        private readonly AppDbContext _context;
        public GenresService(AppDbContext context)
        {
            _context = context;
        }
        public  void Add(Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Genres.FirstOrDefaultAsync(n => n.Id == id);
            _context.Genres.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            var result = await _context.Genres.Include(bg => bg.BookGenres).ToListAsync();
            return result;
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            var result = await _context.Genres.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<Genre> UpdateAsync(Genre newGenre)
        {
            _context.Update(newGenre);
            await _context.SaveChangesAsync();
            return newGenre;
        }
    }
}


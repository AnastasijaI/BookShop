using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Services
{
    public class BooksGenresService : IBooksGenresService
    {
        private readonly AppDbContext _context;
        public BooksGenresService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(BookGenre bookGenre)
        {
            _context.BookGenres.Add(bookGenre);
            _context.SaveChanges();
            
        }
        public async Task<IEnumerable<BookGenre>> GetAll()
        {
            return await _context.BookGenres.ToListAsync();
        }
        public async Task<BookGenre> UpdateAsync(BookGenre bookGenre)
        {
            _context.Update(bookGenre);
            await _context.SaveChangesAsync();
            return bookGenre;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public bool Delete(BookGenre bookGenre)
        {
            _context.BookGenres.Remove(bookGenre);
            return Save();
        }

        
    }
}

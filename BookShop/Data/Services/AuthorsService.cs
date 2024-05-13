
using BookShop.Data;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Authors.FirstOrDefaultAsync(n => n.Id == id);
            _context.Authors.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            var result = await _context.Authors.Include(b => b.Books).ToListAsync();
            return result;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            var result = await _context.Authors.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<Author> UpdateAsync(Author newAuthor)
        {
            _context.Update(newAuthor);
            await _context.SaveChangesAsync();
            return newAuthor;
        }
        //public async Task<IEnumerable<Author>> SearchByLastName(string lastName)
        //{
        //    return await _context.Authors.Include(b => b.Books).Where(a => a.LastName.Contains(lastName)).ToListAsync();
        //}

        //public async Task<IEnumerable<Author>> SearchByName(string firstName)
        //{
        //    return await _context.Authors.Include(b => b.Books).Where(a => a.FirstName.Contains(firstName)).ToListAsync();
        //}
        public async Task<IEnumerable<Author>> SearchByNameOrLastNameAsync(string searchString)
        {
            var result = await _context.Authors
                .Where(a => a.FirstName.Contains(searchString) || a.LastName.Contains(searchString))
                .Include(b => b.Books)
                .ToListAsync();

            return result;
        }
    }
}
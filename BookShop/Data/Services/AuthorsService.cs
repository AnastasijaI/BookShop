using BookShop.Data.Base;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data.Services
{
    public class AuthorsService : EntityBaseRepository<Author>, IAuthorsService
    {
        //private readonly AppDbContext _context;
        public AuthorsService(AppDbContext context) : base(context) { }
        //{
        //    _context = context;
        //}
        //public async Task AddAsync(Author author)
        //{
        //    await _context.Authors.AddAsync(author);
        //    await _context.SaveChangesAsync();
        //}
        //public async Task DeleteAsync(int id)
        //{
        //    var result = await _context.Authors.FirstOrDefaultAsync(n => n.Id == id);
        //    _context.Authors.Remove(result);
        //    await _context.SaveChangesAsync();
        //}
        
        //public async Task<Author> UpdateAsync(int id, Author newAuthor)
        //{
        //    _context.Update(newAuthor);
        //    await _context.SaveChangesAsync();
        //    return newAuthor;
        //}
    }
}

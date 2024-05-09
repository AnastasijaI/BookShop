using BookStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class BookGenresController : Controller
    {
        private readonly AppDbContext _context;

        public BookGenresController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await _context.BookGenres.ToListAsync();
            return View();
        }
    }
}

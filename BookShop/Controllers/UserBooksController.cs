using BookShop.Models;
using BookShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly AppDbContext _context;

        public UserBooksController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allUserBooks = await _context.UserBooks.ToListAsync();
            return View();
        }
    }
}

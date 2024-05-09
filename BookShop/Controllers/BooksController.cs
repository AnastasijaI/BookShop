using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _service;

        public BooksController(AppDbContext service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.Books.Include(n => n.Author).Include(b => b.Reviews).Include(b => b.BookGenres).ToListAsync();
            return View(allBooks);
        }
        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await _service.GetByIdAsync(id);

            if (authorDetails == null) return View("NotFound");
            return View(authorDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title, YearPublished, NumPages, Description, Publisher, Frontpage, DownloadUrl ")]Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }

            await _service.AddAsync(book);
            return RedirectToAction(nameof(Index));
        }
    }
}

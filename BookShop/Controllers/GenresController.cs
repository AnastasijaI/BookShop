using BookShop.Data.Services;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenresService _service;

        public GenresController(IGenresService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allGenres = await _service.GetAllAsync();
            return View();
        }
        //Get: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            await _service.AddAsync(genre);
            return RedirectToAction(nameof(Index));
        }

        //Get: Authors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);

            if (genreDetails == null) return View("NotFound");
            return View(genreDetails);
        }
        //Get: Authors/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);
            if (genreDetails == null) return View("NotFound");
            return View(genreDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            await _service.UpdateAsync(genre);
            return RedirectToAction(nameof(Index));
        }

        //Get: Authors/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);
            if (genreDetails == null) return View("NotFound");
            return View(genreDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);
            if (genreDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}


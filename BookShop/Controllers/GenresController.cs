using BookShop.Data.Services;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

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
            return View(allGenres);
        }
        //Get: Authors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
             _service.Add(genre);
            return RedirectToAction(nameof(Index));
        }

        ////Get: Authors/Details/1
        //public async Task<IActionResult> Details(int id)
        //{
        //    var genreDetails = await _service.GetByIdAsync(id);

        //    if (genreDetails == null) return View("NotFound");
        //    return View(genreDetails);
        //}
        //Get: Authors/Edit
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);
            if (genreDetails == null) return View("NotFound");
            return View(genreDetails);
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        //Get: Authors/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);
            if (genreDetails == null) return View("NotFound");
            return View(genreDetails);
        }
        [Authorize(Roles = "Admin")]
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


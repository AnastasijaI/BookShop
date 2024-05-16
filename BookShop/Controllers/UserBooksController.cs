using BookShop.Models;
using BookShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop.Data.Services;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly IUserBooksService _userBooksService;

        public UserBooksController(IUserBooksService userBooksService)
        {
            _userBooksService = userBooksService;
        }

        public async Task<IActionResult> Index()
        {
            var userBooks = await _userBooksService.GetAllUserBooks();
            return View(userBooks);
        }

        public async Task<IActionResult> BuyBook(int id)
        {
            var result = await _userBooksService.BuyBook(id);
            if (result)
            {
                TempData["Message"] = "Book added to basket successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to add the book to basket.";
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _userBooksService.GetUserById(id);
            if (book == null)
            {
                return View("NotFound");
            }
            return View(book);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteBookConfirmed(int id)
        {
            var userId = await _userBooksService.GetCurrentUserId();
            var result = await _userBooksService.DeleteBookFromUser(userId, id);

            if (!result)
            {
                return View("NotFound");
            }

            return RedirectToAction("Index");
        }
    }
}

using BookShop.Data.Services;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using BookShop.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;
namespace BookShop.Controllers
{
    public class ReviewsController : Controller
    {

        private readonly IReviewsService _service;
        private readonly IUserBooksService _userBooksService;
        private readonly UserManager<BookShopUser> _userManager;
        public ReviewsController(IReviewsService service, UserManager<BookShopUser> userManager, IUserBooksService userBooksService)
        {
            _service = service;
            _userManager = userManager;
            _userBooksService = userBooksService;
        }
        public async Task<IActionResult> Index(int id)
        {
            var allReviews = await _service.GetAllAsync(id);
            return View(allReviews);
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Create(int bookId)
        {
            var review = new Review { BookId = bookId };
            return View(review);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }
            //// Get the current user's ID
            //var currentUser = await _userManager.GetUserAsync(User);
            //review.AppUser = currentUser.Id;

            await _service.AddAsync(review);
            return Redirect("/Reviews/Index/" + review.BookId);
        }
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null)
            {
                return View("NotFound");
            }
            //
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (reviewDetails.AppUser != userId)
            //{
            //    return Forbid();
            //}

            return View(reviewDetails);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }
            //// Get the current user's ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var existingReview = await _service.GetByIdAsync(id);
            //if (existingReview.AppUser != userId)
            //{
            //    return Forbid(); // Вратете 403 Забрането ако корисникот не е автор
            //}


            await _service.UpdateAsync(review);
            return Redirect("/Reviews/Index/" + review.BookId);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");

            // Get the current user's ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (reviewDetails.AppUser != userId || !User.IsInRole("Admin"))
            //{
            //    return Forbid();
            //}

            await _service.DeleteAsync(id);
            return Redirect("/Reviews/Index/" + reviewDetails.BookId);
            //var reviewDetails = await _service.GetByIdAsync(id);
            //if (reviewDetails == null) return View("NotFound");

            ////// Get the current user's ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (reviewDetails.AppUser != userId)
            //{
            //    return Forbid(); 
            //}

            //return View(reviewDetails);
        }
        
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");

            // Get the current user's ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (reviewDetails.AppUser != userId || !User.IsInRole("Admin"))
            //{
            //    return Forbid();
            //}

            await _service.DeleteAsync(id);

            // Redirect to the Reviews Index page with the appropriate bookId
            return RedirectToAction("Index", new { id = reviewDetails.BookId });
           // return Redirect("/Reviews/Index/" + reviewDetails.BookId);

            //var reviewDetails = await _service.GetByIdAsync(id);
            //if (reviewDetails == null) return View("NotFound");

            //// Get the current user's ID
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (reviewDetails.AppUser != userId)
            //{
            //    return Forbid(); 
            //}

            //await _service.DeleteAsync(id);
            //return Redirect("/Reviews/Index/" + reviewDetails.BookId);
        }
    }
}

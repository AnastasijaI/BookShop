using BookShop.Data.Services;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace BookShop.Controllers
{
    public class ReviewsController : Controller
    {

        private readonly IReviewsService _service;
        public ReviewsController(IReviewsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int id)
        {
            var allReviews = await _service.GetAllAsync(id);
            return View(allReviews);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int bookId)
        {
            var review = new Review { BookId = bookId };
            return View(review);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }

            await _service.AddAsync(review);
            return Redirect("/Reviews/Index/" + review.BookId);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null)
            {
                return View("NotFound");
            }
            return View(reviewDetails);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }
            await _service.UpdateAsync(review);
            return Redirect("/Reviews/Index/" + review.BookId);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");
            return View(reviewDetails);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return Redirect("/Reviews/Index/" + reviewDetails.BookId);
        }



        //KODOVI KOI RABOTAT NO GI SMENIV MALKU GI CUVAM VO SLUCAJ AKO SAKAM PAK DA SMENAM NESTO
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(int id, Review review)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(review);
        //    }
        //    var newReview = new Review
        //    {
        //        BookId = id,
        //        AppUser = review.AppUser,
        //        Comment = review.Comment,
        //        Rating = review.Rating,
        //    };
        //     _service.AddAsync(newReview);
        //    return Redirect("/Reviews/Index/" + id);
        //}
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var reviewDetails = await _service.GetByIdAsync(id);
        //    if (reviewDetails == null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(reviewDetails);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, Review review)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(review);
        //    }
        //    await _service.UpdateAsync(review);
        //    return Redirect("/Reviews/Index/" + review.BookId);
        //}
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var reviewDetails = await _service.GetByIdAsync(id);
        //    if (reviewDetails == null) return View("NotFound");
        //    return View(reviewDetails);
        //}
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var reviewDetails = await _service.GetByIdAsync(id);
        //    if (reviewDetails == null) return View("NotFound");

        //    await _service.DeleteAsync(id);
        //    return Redirect("/Reviews/Index/" + reviewDetails.BookId);
        //}







        //private readonly IReviewsService _service;
        //private readonly IBooksService _bookService;
        //public ReviewsController(IReviewsService service, IBooksService bookService)
        //{
        //    _service = service;
        //    _bookService = bookService;
        //}
        //public async Task<IActionResult> Index(int id)
        //{
        //    // var allReviews = await _service.GetByIdAsync(id);
        //    var allReviews = await _service.GetAllAsync();
        //    return View(allReviews);
        //}
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(int id, Review review)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(review);
        //    }

        //    // Проверете дали постои книгата со дадениот BookId
        //    var book = await _bookService.GetByIdAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound(); // Враќа 404 NotFound ако книгата не е пронајдена
        //    }

        //    // Сега може да продолжите со вметнувањето на рецензијата
        //    var newReview = new Review
        //    {
        //        BookId = book.Id,
        //        AppUser = review.AppUser,
        //        Comment = review.Comment,
        //        Rating = review.Rating,
        //    };

        //    await _service.AddAsync(newReview); // Користење на сервисот за креирање на рецензии

        //    return Redirect("/Reviews/Index/" + book); // Пренасочување на контролерот за покажување на рецензии за истата книга
        //}

        ////public async Task<IActionResult> Create(int id, Review review)
        ////{
        ////    if (!ModelState.IsValid)
        ////    {
        ////        return View(review);
        ////    }
        ////    var newReview = new Review
        ////    {
        ////        BookId = id,
        ////        AppUser = review.AppUser,
        ////        Comment = review.Comment,
        ////        Rating = review.Rating,
        ////    };
        ////    _service.AddAsync(newReview);
        ////    return Redirect("/Reviews/Index/" + id);
        ////}
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var reviewDetails = await _service.GetByIdAsync(id);
        //    if (reviewDetails == null)
        //    {
        //        return View("NotFound");
        //    }
        //    return View(reviewDetails);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, Review review)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(review);
        //    }
        //    await _service.UpdateAsync(review);
        //    return RedirectToAction(nameof(Index));
        //}
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var reviewDetails = await _service.GetByIdAsync(id);
        //    if (reviewDetails == null) return View("NotFound");
        //    return View(reviewDetails);
        //}
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var reviewDetails = await _service.GetByIdAsync(id);
        //    if (reviewDetails == null) return View("NotFound");

        //    await _service.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
    }
}

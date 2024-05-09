﻿using BookShop.Data.Services;
using BookShop.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _service;

        public ReviewsController(IReviewsService service, IBooksService bookService)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int id)
        {
            var allReviews = await _service.GetByIdAsync(id);
            return View(allReviews);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }
            var newReview = new Review
            {
                BookId = id,
                AppUser = review.AppUser,
                Comment = review.Comment,
                Rating = review.Rating,
            };
            _service.AddAsync(newReview);
            return Redirect("/Reviews/Index/" + id);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null)
            {
                return View("NotFound");
            }
            return View(reviewDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Review review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }
            await _service.UpdateAsync(review);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");
            return View(reviewDetails);
        }
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

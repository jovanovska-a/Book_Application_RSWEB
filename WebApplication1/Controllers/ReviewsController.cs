using e_shop.Data;
using e_shop.Data.Services;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApplication1.viewModel;

namespace e_shop.Controllers
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
            var allReviews = await _service.GetAllById(id);
            ReviewsViewModel newRVM = new ReviewsViewModel()
            {
                BookId = id,
                Reviews = allReviews
            };
            return View(newRVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(int id,Review review)
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
            _service.Add(newReview);
            return Redirect("/Reviews/Index/"+id);
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
            return Redirect("/Reviews/Index/" +review.BookId);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");
            return View(reviewDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviewDetails = await _service.GetByIdAsync(id);
            if (reviewDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return Redirect("/Reviews/Index/" + reviewDetails.BookId);
        }
    }
}

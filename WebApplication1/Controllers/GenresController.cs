using e_shop.Data;
using e_shop.Data.Services;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_shop.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenresService _service;
        public GenresController(IGenresService service)
        {
            _service=service;
        }
        public async Task<IActionResult> Index()
        {
            var allGenres = await _service.GetAll();
            return View(allGenres);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("GenreName")] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View(genre);
            }
            _service.Add(genre);
            return RedirectToAction(nameof(Index));
        }

        //Actors/Delete
        public async Task<IActionResult> Delete(int id)
        {
            var genreDetails=await _service.GetByIdAsync(id);
            if (genreDetails == null) return View("NotFound");
            return View(genreDetails);
        }
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);
            if (genreDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var genreDetails = await _service.GetByIdAsync(id);
            if (genreDetails == null)
            {
                return View("NotFound");
            }
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
    }
}

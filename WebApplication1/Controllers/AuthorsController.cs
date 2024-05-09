using e_shop.Data;
using e_shop.Data.Services;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_shop.Controllers
{
    public class AuthorsController : Controller
    {

            private readonly IAuthorsService _service;
            public AuthorsController(IAuthorsService service)
            {
                _service=service;
            }
            public async Task<IActionResult> Index()
            {
                var allAuthors = await _service.GetAll();
                return View(allAuthors);
            }
        //Get: Books/Create
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(Author author)
            {
                if (!ModelState.IsValid)
                {
                    return View(author);
                }
                _service.Add(author);
                return RedirectToAction(nameof(Index));
            }

            //Get: Edit
            public async Task<IActionResult> Edit(int id)
            {
                var actorDetails = await _service.GetByIdAsync(id);
                if(actorDetails == null)
                {
                    return View("NotFound");
                }
                return View(actorDetails);
            }
            [HttpPost]
            public async Task<IActionResult> Edit(int id, Author author)
            {
                if (!ModelState.IsValid)
                {
                    return View(author);
                }
                await _service.UpdateAsync(author);
                return RedirectToAction(nameof(Index));
            }
            public async Task<IActionResult> Delete(int id)
            {
                var authorDetails = await _service.GetByIdAsync(id);
                if (authorDetails == null) return View("NotFound");
                return View(authorDetails);
            }
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var authorDetails = await _service.GetByIdAsync(id);
                if (authorDetails == null) return View("NotFound");

                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
    }
}

using e_shop.Data;
using e_shop.Data.Services;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_shop.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _service;
        public BooksController(IBooksService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync();
            return View(allBooks);
        }
        //Get: Books/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title")] Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }
            await _service.AddAsync(book);
            return RedirectToAction(nameof(Index));
        }
        //Get: Books/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if(actorDetails==null)
            {
                return View("Empty");  
            }
            return View(actorDetails);
        }

    }
}


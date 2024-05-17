using e_shop.Data;
using e_shop.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data.Services;

namespace WebApplication1.Controllers
{
    public class UserBooksController : Controller
    {
        public readonly IUserBooksService _userBooksService;
        public UserBooksController(IUserBooksService userBooksService)
        {
            _userBooksService=userBooksService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> myBooks = await _userBooksService.GetAllUserBooks();
            return View(myBooks);
        }
        public async Task<IActionResult> BuyBook(int id)
        { 
            var curUser = _userBooksService.GetCurrentUserId();
            bool isBook = _userBooksService.BookExists(id);
            if(isBook) { return Redirect("/Books/Index/"); }
            UserBook newUB = new UserBook()
            {
                AppUser =curUser,
                BookId = id,
            };
            _userBooksService.Add(newUB);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var userBook = _userBooksService.GetUserBook(id);
            _userBooksService.Delete(userBook);
            return RedirectToAction("Index");
        }
    }
}

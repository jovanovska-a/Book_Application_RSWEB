using e_shop.Data;
using e_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Services
{
    public class UserBooksService : IUserBooksService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserBooksService(AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor= httpContextAccessor;
        }
        public bool Add(UserBook userBooks)
        {
            _context.UserBooks.Add(userBooks);
            return Save();
        }

        public bool Delete(UserBook userBooks)
        {
            _context.UserBooks.Remove(userBooks);
            return Save();
        }

        public async Task<List<Book>> GetAllUserBooks()
        {
            var curUser = GetCurrentUserId();
            List<UserBook> userBooks = await _context.UserBooks.Where(u => u.AppUser == curUser).ToListAsync();
            List<Book> myBooks = new List<Book>();
            foreach (var book in userBooks)
            {
                var myBook = await _context.Books.Include(a => a.Author).Include(r => r.Reviews).Include(bg => bg.Books_Genres).ThenInclude(g => g.Genre).FirstOrDefaultAsync(i => i.Id == book.BookId);
                myBooks.Add(myBook);
            }
            return myBooks;
        }
        public UserBook GetUserBook(int id)
        {
            var curUser = GetCurrentUserId();
            UserBook userBook=_context.UserBooks.FirstOrDefault(a => a.AppUser == curUser && a.BookId == id);
            return userBook;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User.GetUserId();
        }

        public string GetCurrentUserUsername()
        {
            var curUser = GetCurrentUserId();
            var username = _context.Users.FirstOrDefault(i => i.Id == curUser).UserName;
            return username;
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(UserBook userBooks)
        {
            _context.UserBooks.Update(userBooks);
            return Save();
        }
        public bool BookExists(int id)
        {
            var curUser = GetCurrentUserId();
            UserBook userBook = _context.UserBooks.FirstOrDefault(a => a.AppUser == curUser && a.BookId == id);
            if (userBook != null)
                return true;
            else
                return false;
        }
        public bool HasBook(int bookId)
        {
            var curUser = GetCurrentUserId();
            if (curUser == null)
            {
                var hasBook = false;
                return hasBook;
            }
            else
            {
                var hasBook = _context.UserBooks.FirstOrDefault(u => u.AppUser == curUser && u.BookId == bookId);
                return hasBook != null ? true : false;
            }
        }
    }
}

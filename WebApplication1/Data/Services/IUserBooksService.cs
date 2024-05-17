using e_shop.Models;

namespace WebApplication1.Data.Services
{
    public interface IUserBooksService
    {
        Task<List<Book>> GetAllUserBooks();
        string GetCurrentUserId();
        string GetCurrentUserUsername();
        bool HasBook(int bookId);
        Task<AppUser> GetUserById(string id);
        bool Add(UserBook userBooks);
        bool Update(UserBook userBooks);
        bool Delete(UserBook userBooks);
        bool Save();
        UserBook GetUserBook(int id);
        bool BookExists(int id);
    }
}

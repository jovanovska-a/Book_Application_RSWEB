using e_shop.Models;

namespace e_shop.Data.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Book Update(int id,Book book);
        void Delete(int id);
    }
}

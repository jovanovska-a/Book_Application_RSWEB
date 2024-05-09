using e_shop.Models;

namespace e_shop.Data.Services
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task<Book> UpdateAsync(int id,Book book);
        Task DeleteAsync(int id);
        Task<Book> GetLastBook();
        Task<Book> GetByIdAsyncNoTracking(int id); 
        Task<IEnumerable<Author>> GetAllAuthors();
        Task<IEnumerable<Genre>> GetAllGenres();


    }
}

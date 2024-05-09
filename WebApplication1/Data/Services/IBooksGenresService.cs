using e_shop.Models;

namespace WebApplication1.Data.Services
{
    public interface IBooksGenresService
    {
        Task<IEnumerable<BookGenre>> GetAll();
        void Add(BookGenre bookGenre);
        Task<BookGenre> UpdateAsync(BookGenre bookGenre);
        bool Delete(BookGenre bookGenre);
        bool Save();
    }
}

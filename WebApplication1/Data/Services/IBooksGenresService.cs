using e_shop.Models;

namespace WebApplication1.Data.Services
{
    public interface IBooksGenresService
    {
        void Add(BookGenre bookGenre);
        Task<BookGenre> UpdateAsync(BookGenre bookGenre);
    }
}

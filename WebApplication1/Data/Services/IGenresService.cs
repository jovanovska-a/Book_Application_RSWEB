using e_shop.Models;

namespace e_shop.Data.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetByIdAsync(int id);
        void Add(Genre genre);
        Task<Genre> UpdateAsync(Genre genre);
        Task DeleteAsync(int id);
    }
}


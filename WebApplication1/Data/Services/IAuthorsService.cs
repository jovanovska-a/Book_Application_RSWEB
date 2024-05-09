using e_shop.Models;

namespace e_shop.Data.Services
{
    public interface IAuthorsService
    {
        Task<IEnumerable<Author>> GetAll();
        Task<Author> GetByIdAsync(int id);
        void Add(Author author);
        Task<Author> UpdateAsync(Author author);
        Task DeleteAsync(int id);
    }
}


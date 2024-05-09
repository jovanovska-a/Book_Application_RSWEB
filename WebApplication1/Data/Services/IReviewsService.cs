using e_shop.Models;

namespace e_shop.Data.Services
{
    public interface IReviewsService
    {
        Task<IEnumerable<Review>> GetAllById(int id);
        Task<Review> GetByIdAsync(int id);
        void Add(Review review);
        Task<Review> UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
}


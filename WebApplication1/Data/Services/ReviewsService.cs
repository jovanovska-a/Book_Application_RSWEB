using e_shop.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1;

namespace e_shop.Data.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewsService(AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor=httpContextAccessor;
            _context = context;
        }
        public void Add(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(n => n.Id == id);
            _context.Reviews.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetAllById(int id)
        {
            var result = await _context.Reviews.Include(a => a.AppUserInfo).Where(b => b.BookId.Equals(id)).ToListAsync();
            return result;
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }
        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User.GetUserId();
        }
        public string GetCurrentUserUsername(string id)
        {
            var username = _context.Users.FirstOrDefault(i => i.Id == id).UserName;
            return username;
        }

        public async Task<Review> UpdateAsync(Review review)
        {
            _context.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }

    
    }
}

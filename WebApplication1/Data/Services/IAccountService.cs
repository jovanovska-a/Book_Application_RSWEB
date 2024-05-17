using e_shop.Models;

namespace WebApplication1.Data.Services
{
    public interface IAccountService
    {
        string GetCurrentUserId();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}

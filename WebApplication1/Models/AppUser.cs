using Microsoft.AspNetCore.Identity;

namespace e_shop.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserBook> Users_Books { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace BookShop.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
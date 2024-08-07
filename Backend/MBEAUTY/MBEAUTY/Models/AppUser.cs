using Microsoft.AspNetCore.Identity;

namespace MBEAUTY.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}

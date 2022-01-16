using Microsoft.AspNetCore.Identity;

namespace MovieInfo.EntityFramework
{
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }
    }
}

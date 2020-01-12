using Microsoft.AspNetCore.Identity;

namespace KeyForgeGameTracker.Areas.Identity.Models
{
    public class AppRole : IdentityRole<int>
    {
        public AppRole() { }

        public AppRole(string name)
        {
            Name = name;
        }
    }
}

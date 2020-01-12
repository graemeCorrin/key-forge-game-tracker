using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KeyForgeGameTracker.Areas.Identity.Models;
using KeyForgeGameTracker.Util;

namespace KeyForgeGameTracker.Areas.Identity.Pages.Account.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {

        private readonly UserManager<AppUser> _userManager;

        public List<AppUser> AdminUsers { get; set; }
        public List<AppUser> StandardUsers { get; set; }
        public List<AppUser> ReadOnlyUsers { get; set; }

        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            AdminUsers = (await _userManager.GetUsersInRoleAsync(Role.Admin)).ToList();
            StandardUsers = (await _userManager.GetUsersInRoleAsync(Role.Standard)).ToList();
            ReadOnlyUsers = (await _userManager.GetUsersInRoleAsync(Role.ReadOnly)).ToList();
            return Page();
        }
    }
}
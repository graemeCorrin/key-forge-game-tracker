using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KeyForgeGameTracker.Areas.Identity.Models;
using KeyForgeGameTracker.Util;

namespace KeyForgeGameTracker.Areas.Identity.Pages.Account.Admin
{
    [Authorize(Roles = Role.Admin)]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public SelectList Roles { get; set; }

        public EditUserModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public bool IsEmailConfirmed { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [EmailAddress]
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Role")]
            public string Role { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                return NotFound($"Unable to load user '{user.Email}'");
            }

            Roles = Role.GetRoleDropdownList();
            await LoadUser(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Roles = Role.GetRoleDropdownList();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Username);
            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            await _userManager.UpdateAsync(user);


            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, Input.Role);

            TempData["SuccessMessage"] = $"Profile for {Input.FirstName} {Input.LastName} has been updated";
            return RedirectToPage("./Index");
        }

        private async Task LoadUser(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var role = (await _userManager.GetRolesAsync(user))[0];

            Input = new InputModel
            {
                Username = userName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = role
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }
    }
}
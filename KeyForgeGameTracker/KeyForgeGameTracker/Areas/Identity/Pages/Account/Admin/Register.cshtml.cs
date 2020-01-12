using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using KeyForgeGameTracker.Areas.Identity.Models;
using KeyForgeGameTracker.Services;
using KeyForgeGameTracker.Util;

namespace KeyForgeGameTracker.Areas.Identity.Pages.Account
{
    [Authorize(Roles = Role.Admin)]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailService _emailService;
        private readonly IPasswordGeneratorService _passwordGenerator;

        public SelectList Roles { get; set; }

        public RegisterModel(
            UserManager<AppUser> userManager,
            ILogger<RegisterModel> logger,
            IEmailService emailService,
            IPasswordGeneratorService passwordGenerator)
        {
            _userManager = userManager;
            _logger = logger;
            _emailService = emailService;
            _passwordGenerator = passwordGenerator;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }
       
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

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

        public void OnGet(string returnUrl = null)
        {
            Roles = Role.GetRoleDropdownList();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Roles = Role.GetRoleDropdownList();

            if (ModelState.IsValid)
            {
                var user = new AppUser {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName
                };

                var result = await _userManager.CreateAsync(user, _passwordGenerator.GeneratePassword(32));
                _ = await _userManager.AddToRoleAsync(user, Input.Role);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var confirmCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page("/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = confirmCode },
                        protocol: Request.Scheme);

                    await _emailService.NewAccountAsync(Input.Email, callbackUrl);

                    TempData["SuccessMessage"] = $"An email has been sent to {Input.Email}";
                    return RedirectToPage("./Index");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

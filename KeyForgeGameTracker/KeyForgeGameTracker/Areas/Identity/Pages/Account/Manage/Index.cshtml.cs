﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KeyForgeGameTracker.Areas.Identity.Models;
using KeyForgeGameTracker.Services;

namespace KeyForgeGameTracker.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
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

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadUser(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;

            await LoadUser(user);
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            TempData["SuccessMessage"] = $"Your profile has been updated";
            return Page();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadUser(user);

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailService.ConfirmEmailAsync(email, callbackUrl);

            TempData["SuccessMessage"] = $"Verification email sent. Please check your email.";
            return Page();
        }

        private async Task LoadUser(AppUser user)
        {
           
            var userName = await _userManager.GetUserNameAsync(user);

            Input = new InputModel
            {
                Username = userName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }
    }
}

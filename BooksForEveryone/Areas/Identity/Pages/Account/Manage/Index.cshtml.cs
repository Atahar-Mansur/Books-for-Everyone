using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BooksForEveryone.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksForEveryone.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;  //change
        private readonly SignInManager<ApplicationUser> _signInManager;  //change
        private readonly IEmailSender _emailSender;

        public IndexModel(
            UserManager<ApplicationUser> userManager,  //change
            SignInManager<ApplicationUser> signInManager,  //change
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            //Change
            //User Primery Info
            [Required]
            public string Name { get; set; }
            [Required, MaxLength(11)]
            [Display(Name = "Mobile number")]
            public string MobileNumber { get; set; }

            //Present address
            [Required]
            public string Address { get; set; }
            [Required]
            [Display(Name = "Zip Code")]
            public int ZipCode { get; set; }
            [Required]
            [Display(Name = "Area/Thana")]
            public string AreaThana { get; set; }
            [Required]
            public string District { get; set; }

            //Initial Books Info
            [Required]
            [Display(Name = "Name")]
            public string Book1Name { get; set; }
            [Required]
            [Display(Name = "Writter Name")]
            public string Book1WriName { get; set; }

            [Display(Name = "Name")]
            public string Book2Name { get; set; }
            [Display(Name = "Writter Name")]
            public string Book2WriName { get; set; }

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                //change
                Name = user.Name,
                MobileNumber = user.MobileNumber,
                Address = user.Address,
                ZipCode = user.ZipCode,
                AreaThana = user.AreaThana,
                District = user.District,
                Book1Name = user.Book1Name,
                Book1WriName = user.Book1WriName,
                Book2Name = user.Book2Name,
                Book2WriName = user.Book2WriName
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

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

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            //change
            if(Input.Name != user.Name)
            {
                user.Name = Input.Name;
                await _userManager.UpdateAsync(user);
            }
            if (Input.MobileNumber != user.MobileNumber)
            {
                user.MobileNumber = Input.MobileNumber;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Address != user.Address)
            {
                user.Address = Input.Address;
                await _userManager.UpdateAsync(user);
            }
            if (Input.ZipCode != user.ZipCode)
            {
                user.ZipCode = Input.ZipCode;
                await _userManager.UpdateAsync(user);
            }
            if (Input.AreaThana != user.AreaThana)
            {
                user.AreaThana = Input.AreaThana;
                await _userManager.UpdateAsync(user);
            }
            if (Input.District != user.District)
            {
                user.District = Input.District;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Book1Name != user.Book1Name)
            {
                user.Book1Name = Input.Book1Name;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Book1WriName != user.Book1WriName)
            {
                user.Book1WriName = Input.Book1WriName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Book2Name != user.Book2Name)
            {
                user.Book2Name = Input.Book2Name;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Book2WriName != user.Book2WriName)
            {
                user.Book2WriName = Input.Book2WriName;
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
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


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}

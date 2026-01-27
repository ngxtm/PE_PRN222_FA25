using BLL.Services;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RealEstateManagement_MinhNTSE196686.Pages
{
    public class LoginModel : PageModel
    {
        private readonly GenericService<SystemUser> _systemUserService;

        public LoginModel(GenericService<SystemUser> systemUserService)
        {
            _systemUserService = systemUserService;
        }

        public List<SystemUser> SystemUser { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Username is required")]
            public string? UserName { get; set; }
            [Required(ErrorMessage = "Password is required")]
            public string? Password { get; set; }
        }
        //[BindProperty]
        //public string UserName { get; set; }
        //[BindProperty]
        //public string Password { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _systemUserService.GetAll(
                filter: u => u.Username == Input.UserName && u.UserPassword == Input.Password
            ).FirstOrDefault();

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserRole", user.UserRole);
                HttpContext.Session.SetString("UserName", user.Username);
                return RedirectToPage("/Contract/Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return Page();
            }
        }
    }
}

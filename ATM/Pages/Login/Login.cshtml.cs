using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ATM.ApplicationContext;
using ATM.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATM.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _appContext;

        public LoginModel(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }
        [BindProperty]
        public UserLogin UserLogin { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            /*if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _appContext.User.FirstOrDefault(user => user.Username.Equals(UserLogin.Username) && (user.Password.Equals(UserLogin.Password) || user.Password.Equals(UserLogin.Pin)));

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserLogged", user.Id);
                var scheme = CookieAuthenticationDefaults.AuthenticationScheme;

                var User = new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new[] { new Claim(ClaimTypes.Name, UserLogin.Username) },
                        scheme
                        )
                    );
                return RedirectToPage("../Index");
            }
            else
            {
                ViewData["Message"] = "Email or Password no match";
                return Page();
            }*/


            var user = _appContext.User.Where(login => UserLogin.Username.Equals(login.Username) && (UserLogin.Password.Equals(login.Password) || UserLogin.Password.Equals(login.Pin)));
            if (user.Any())
            {

                HttpContext.Session.SetString("Username", UserLogin.Username);
                HttpContext.Session.SetInt32("Balance", user.Select(x => x.Balance).First());
                return RedirectToPage("../Index");
            }
            else
            {
                return Page();
            }
        }
    }
}

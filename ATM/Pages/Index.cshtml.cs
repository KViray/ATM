using ATM.ApplicationContext;
using ATM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _appContext;

        public IndexModel(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public string Username { get; set; } 
        public int? Balance { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
            Balance = HttpContext.Session.GetInt32("Balance");
        }
        public async Task<IActionResult> OnPostDebit()
        {
            var username = _appContext.User.Where(user => user.Username == HttpContext.Session.GetString("Username"));
            if (username.Any())
            {
                username.FirstOrDefault().Balance += Amount;
                Balance = username.FirstOrDefault().Balance;
               await  _appContext.SaveChangesAsync();
                ModelState.Clear();
                return Page();
            }
            else
            {
                return null;
            }
        }
        public async Task<IActionResult> OnPostCredit()
        {
            var username = _appContext.User.Where(user => user.Username == HttpContext.Session.GetString("Username"));
            if (username.Any())
            {
                username.FirstOrDefault().Balance -= Amount;
                Balance = username.FirstOrDefault().Balance;
                await _appContext.SaveChangesAsync();
                ModelState.Clear();
                return Page();
            }
            else
            {
                return null;
            }

        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login/Login");
        }
    }
}

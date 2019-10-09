using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web;
using web.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public bool RememberMe { get; set; }

        public LoginModel()
        {
            
        }
        
        public void OnGetAsync()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || !IsValid(Username, Password))
            {
                return Page();
            }

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, Username));
            identity.AddClaim(new Claim(ClaimTypes.Name, Username));
            var principal = new ClaimsPrincipal(identity);
            var authProperties = new AuthenticationProperties { 
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                IsPersistent = RememberMe
            };
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
            
            return RedirectToPage("/Index");
        }

        private bool IsValid(string username, string password)
        {
            // return _accountService.ValidateLogin(username, password).Result;
            return true;
        }
    }
}

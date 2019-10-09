using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web;
using web.Domain;
using Microsoft.AspNetCore.Authentication;

namespace web.Pages
{
    public class AccountModel : PageModel
    {

        public AccountModel()
        {
            
        }
        
        public void OnGetAsync()
        {
            
        }
        
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync();
            
            return RedirectToPage("/Index");
        }
    }
}

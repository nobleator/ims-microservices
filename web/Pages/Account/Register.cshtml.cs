using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web;
using web.Domain;
using System.ComponentModel.DataAnnotations;

namespace web.Pages
{
    // Must use ViewModel class due to bug described here: https://github.com/aspnet/AspNetCore/issues/4895
    public class ViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Compare(nameof(Password), ErrorMessage="Passwords don't match")]
        public string ConfirmPassword { get; set; }
        public List<string> Errors { get; set; }
    }
    
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public ViewModel ViewModel { get; set; }
        
        public RegisterModel()
        {
            
        }
        
        public void OnGetAsync()
        {
            ViewModel = new ViewModel();
            ViewModel.Errors = new List<string>();
        }

        public IActionResult OnPostAsync()
        {
            // Any errors in the registration process are returned as a list of strings
            // ViewModel.Errors = await _accountService.RegisterAccount(ViewModel.Username, ViewModel.Password);
            ViewModel.Errors = new List<string>();

            if (!ModelState.IsValid || ViewModel.Errors.Count > 0)
            {
                return Page();
            }

            return RedirectToPage("/Account/Login");
        }
    }
}

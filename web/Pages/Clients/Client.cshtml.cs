using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web;
using web.Domain;
using web.Domain.DataTransferObjects;
using web.Utility;

namespace web.Pages
{
    public class ClientModel : PageModel
    {
        [BindProperty]
        public Client Client { get; set; }

        public ClientModel()
        {
            
        }
        
        public void OnGetAsync(int id)
        {
            Client = ApiHelper.GetClientById(id).Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // await _clientService.UpdateClient(Client, User.Identity.Name);
            await ApiHelper.UpdateClient(Client);
            return RedirectToPage("/Clients/Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await ApiHelper.DeleteClient(id);
            return RedirectToPage("/Clients/Index");
        }
    }
}

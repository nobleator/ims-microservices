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
    public class ClientsModel : PageModel
    {
        public List<Client> AllClients = new List<Client>();

        public ClientsModel()
        {
            
        }
        
        public void OnGetAsync()
        {
            AllClients = ApiHelper.GetClients().Result;
        }
    }
}

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
    public class ProductsModel : PageModel
    {
        public List<Product> AllProducts = new List<Product>();
        public ProductsModel()
        {
            
        }
        
        public void OnGetAsync()
        {
            AllProducts = ApiHelper.GetProducts().Result;
        }
    }
}

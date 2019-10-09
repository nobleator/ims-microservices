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
    public class ProductModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }
        public ProductModel()
        {
            
        }
        
        public void OnGetAsync(int id)
        {
            Product = ApiHelper.GetProductById(id).Result;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // await _productService.UpdateProduct(Product, User.Identity.Name);
            await ApiHelper.UpdateProduct(Product);
            return RedirectToPage("/Products/Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await ApiHelper.DeleteProduct(id);
            return RedirectToPage("/Products/Index");
        }
    }
}

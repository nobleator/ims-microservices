using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using web;
using web.Domain;
using web.Domain.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using web.Enums;
using web.Utility;
using Newtonsoft.Json;

namespace web.Pages
{
    public class TransactionModel : PageModel
    {
        [BindProperty]
        public Transaction Transaction { get; set; }
        public List<SelectListItem> SelectableProducts = new List<SelectListItem>();
        public List<SelectListItem> SelectableClients = new List<SelectListItem>();
        public TransactionModel()
        {
            
        }
        
        public void OnGetAsync(int id)
        {
            Transaction = ApiHelper.GetTransactionById(id).Result;

            if (Transaction.AssociatedClient == null)
            {
                Console.WriteLine("DEBUG: Warning: AssociatedClient is null");
                // Transaction.AssociatedClient = _clientService.GetDefaultClient();
                Transaction.AssociatedClient = new Client();
            }

            SelectableProducts = ApiHelper.GetProducts().Result
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Name
                })
                .ToList();

            SelectableClients = ApiHelper.GetClients().Result
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.Name
                })
                .ToList();
        }

        public PartialViewResult OnGetNewTableRow()
        {
            // TODO: Set SelectableProducts in constructor?
            var allProducts = ApiHelper.GetProducts().Result;
            
            SelectableProducts = allProducts
                .Select(p => new SelectListItem
                {
                    Value = p.ProductId.ToString(),
                    Text = p.Name
                })
                .ToList();

            var newLineItem = new LineItem {
                LineItemId = -1,
                TransactionId = -1,
                ProductId = allProducts.First().ProductId,
                Quantity = 0,
                Price = 0
            };

            var viewModel = new LineItemViewModel
            {
                LineItem = newLineItem,
                SelectableProducts = SelectableProducts
            };

            // TODO: Why does this work but not return Partial("_LineItemsTablePartial", viewModel) ?
            return new PartialViewResult()
            {
                ViewName = "_LineItemsTablePartial",
                ViewData = new ViewDataDictionary<LineItemViewModel>(ViewData, viewModel)
            };
        }

        public JsonResult OnGetSearchableSiteNames(string query)
        {
            // TODO: Make async
            Console.WriteLine($"DEBUG: Incoming query: {query}");
            var searchResults = ApiHelper.GetSiteSearchResults(query).Result;
            Console.WriteLine($"DEBUG: Site search results: {JsonConvert.SerializeObject(searchResults, Formatting.Indented)}");
            return new JsonResult(searchResults);
        }

        public ContentResult OnGetApiKey()
        {
            // return Content(_transactionService.GetApiKey());
            return Content("dummy_api_key_goes_here");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // await _transactionService.UpdateTransaction(Transaction, User.Identity.Name);
            await ApiHelper.UpdateTransaction(Transaction);
            // TODO: Redirect a different page or not?
            return RedirectToPage("/Transactions/Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await ApiHelper.DeleteTransaction(id);
            return RedirectToPage("/Transactions/Index");
        }
    }
}

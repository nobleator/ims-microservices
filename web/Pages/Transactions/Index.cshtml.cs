using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web;
using web.Domain.DataTransferObjects;
using web.Utility;

namespace web.Pages
{
    public class TransactionsModel : PageModel
    {
        public List<Transaction> AllTransactions = new List<Transaction>();

        public TransactionsModel()
        {
            
        }
        
        public void OnGet()
        {
            AllTransactions = ApiHelper.GetTransactions().Result;
        }
    }
}

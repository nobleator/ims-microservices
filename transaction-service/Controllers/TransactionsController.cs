using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using transaction_service.Domain;
using transaction_service.Domain.Entities;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        // GET api/transactions
        [HttpGet]
        public ActionResult<List<DTO.Transaction>> Get()
        {
            List<DTO.Transaction> transactionList;

            using (var db = new TransactionServiceDbContext())
            {
                transactionList = db.Transactions
                    .Include(t => t.LineItems)
                    .Include(t => t.AssociatedClient)
                    .Where(t => !t.Deleted)
                    .Select(t => t.toDto())
                    .ToList();
            }

            return Ok(transactionList);
        }

        // GET api/transactions/5
        [HttpGet("{id}")]
        public ActionResult<DTO.Transaction> Get(int id)
        {
            DTO.Transaction transaction;

            using (var db = new TransactionServiceDbContext())
            {
                transaction = db.Transactions
                    .Include(t => t.LineItems)
                    .Include(t => t.AssociatedClient)
                    .Where(t => !t.Deleted &&
                                t.TransactionId == id)
                    .Select(t => t.toDto())
                    .FirstOrDefault();
            }
            // Set default for new transactions
            if (transaction == null)
            {
                transaction = new DTO.Transaction
                {
                    TransactionId = -1,
                    TransactionType = Enums.TransactionTypeEnum.Sale,
                    Status = "Draft",
                    LineItems = new List<DTO.LineItem>()
                };
            }

            return transaction;
        }

        // POST api/transactions
        [HttpPost]
        public void Post([FromBody] DTO.Transaction transaction)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(Post)}");
            Console.WriteLine($"DEBUG: Request body contained: {JsonConvert.SerializeObject(transaction, Formatting.Indented)}");
            // TODO: updatedBy
            var updatedBy = "SYSTEM";
            using (var db = new TransactionServiceDbContext())
            {
                // TODO: Only change Updated* values if the other values are actually different. Override SaveChanges method?
                var dbTransaction = db.Transactions.FindAsync(transaction.TransactionId).Result;
                if (dbTransaction == null && transaction.TransactionId == -1)
                {
                    Console.WriteLine($"DEBUG: Creating new transaction");
                    // PK is serial, so should be automatically generated
                    dbTransaction = new Domain.Entities.Transaction
                    {
                        CreatedOn = DateTime.UtcNow,
                        CreatedBy = updatedBy
                    };
                    db.Add(dbTransaction);
                }
                if (dbTransaction != null)                                                         
                {
                    Console.WriteLine("DEBUG: Updating transaction properties");
                    dbTransaction.Status = transaction.Status;
                    dbTransaction.TransactionType = transaction.TransactionType.ToString();
                    dbTransaction.UpdatedOn = DateTime.UtcNow;
                    dbTransaction.UpdatedBy = updatedBy;
                    dbTransaction.DeliverAfter = transaction.DeliverAfter;
                    dbTransaction.DeliverBefore = transaction.DeliverBefore;
                    dbTransaction.Priority = (int)transaction.Priority;
                    dbTransaction.SiteName = transaction.SiteName;
                    dbTransaction.SiteLatitude = transaction.SiteLatitude;
                    dbTransaction.SiteLongitude = transaction.SiteLongitude;

                    // Unsure why LineItems is null instead of empty list
                    if (transaction.LineItems != null)
                    {
                        Console.WriteLine($"DEBUG: Updating {transaction.LineItems.Count} line items");
                        foreach (var lineItem in transaction.LineItems)
                        {
                            var dbLineItem = db.LineItems.FindAsync(lineItem.LineItemId).Result;
                            if (dbLineItem == null)
                            {
                                // Transaction IDs start empty for new incoming line items
                                dbLineItem = new Domain.Entities.LineItem
                                {
                                    LineItemId = lineItem.LineItemId,
                                    TransactionId = dbTransaction.TransactionId,
                                    CreatedOn = DateTime.UtcNow,
                                    CreatedBy = updatedBy
                                };
                                db.Add(dbLineItem);
                            }
                            dbLineItem.ProductId = lineItem.ProductId;
                            dbLineItem.Quantity = lineItem.Quantity;
                            dbLineItem.Price = lineItem.Price;
                            dbLineItem.UpdatedOn = DateTime.UtcNow;
                            dbLineItem.UpdatedBy = updatedBy;
                        }
                    }
                    
                    Console.WriteLine($"DEBUG: Updating client ID");
                    dbTransaction.ClientId = transaction.AssociatedClient?.ClientId;
                    Console.WriteLine($"DEBUG: Saving changes");
                    db.SaveChangesAsync();
                }
            }
            Console.WriteLine($"DEBUG: Exiting {nameof(Post)}");
        }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/transactions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new TransactionServiceDbContext())
            {
                var dbTransaction = db.Transactions.FindAsync(id).Result;
                if (dbTransaction != null)
                {
                    dbTransaction.Deleted = true;
                }
                // TODO: Delete line items as well?
                db.SaveChangesAsync();
            }
        }

        // GET api/transactions/sitesearch/test
        [Route("SiteSearch/{query}")]
        [HttpGet]
        public ActionResult<List<DTO.SiteSearchResult>> SiteSearch(string query)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(SiteSearch)}");
            Console.WriteLine($"DEBUG: query: {query}");
            List<DTO.SiteSearchResult> sites;

            using (var db = new TransactionServiceDbContext())
            {
                var comparer = new SiteSearchComparer(query);
                sites = db.Transactions
                    .Where(t => !t.Deleted)
                    .OrderBy(t => t.SiteName, comparer)
                    .Take(10)
                    .Select(t => new DTO.SiteSearchResult
                    {
                        SiteName = t.SiteName,
                        SiteLatitude = t.SiteLatitude,
                        SiteLongitude = t.SiteLongitude,
                    })
                    .ToList();
            }
            Console.WriteLine($"DEBUG: Matching sites found: {JsonConvert.SerializeObject(sites, Formatting.Indented)}");

            return Ok(sites);
        }
    }

    #region Helpers
    public class SiteSearchComparer : IComparer<string>
    {
        private string _query { get; set; }
        private int _threshold { get; set; }
        
        public SiteSearchComparer(string query, int threshhold = 100)
        {
            _query = query;
            _threshold = threshhold;
        }

        public int Compare(string x, string y)
        {
            // Check if the strings are the same
            if (x == y) return 0;
            // Check if either string exactly matches the query
            if (String.Equals(x, _query)) return -1;
            if (String.Equals(y, _query)) return 1;
            // Check if either string exactly contains the query
            // If both contain the query, return the shorter length string
            if (x.Contains(_query) && y.Contains(_query)) return x.Length - y.Length;
            if (x.Contains(_query)) return -1;
            if (y.Contains(_query)) return 1;
            // Finally, sum edit distances for each word in each string
            // Note: if this has perfomance issues then use the following instead:
            // var xEditDist = DamerauLevenshteinDistance(x, _query, _threshold);
            var xEditDist = x.Split().Sum(word => DamerauLevenshteinDistance(word, _query, _threshold));
            var yEditDist = y.Split().Sum(word => DamerauLevenshteinDistance(word, _query, _threshold));
            return xEditDist - yEditDist;
        }

        // Courtesy of: https://stackoverflow.com/questions/9453731/how-to-calculate-distance-similarity-measure-of-given-2-strings
        private static int DamerauLevenshteinDistance(string source, string target, int threshold)
        {
            int length1 = source.Length;
            int length2 = target.Length;

            // Return trivial case - difference in string lengths exceeds threshhold
            if (Math.Abs(length1 - length2) > threshold)
            {
                return int.MaxValue;
            }

            // Ensure arrays [i] / length1 use shorter length 
            if (length1 > length2)
            {
                Swap(ref target, ref source);
                Swap(ref length1, ref length2);
            }

            int maxi = length1;
            int maxj = length2;

            int[] dCurrent = new int[maxi + 1];
            int[] dMinus1 = new int[maxi + 1];
            int[] dMinus2 = new int[maxi + 1];
            int[] dSwap;

            for (int i = 0; i <= maxi; i++)
            {
                dCurrent[i] = i;
            }

            int jm1 = 0, im1 = 0, im2 = -1;

            for (int j = 1; j <= maxj; j++)
            {
                // Rotate
                dSwap = dMinus2;
                dMinus2 = dMinus1;
                dMinus1 = dCurrent;
                dCurrent = dSwap;

                // Initialize
                int minDistance = int.MaxValue;
                dCurrent[0] = j;
                im1 = 0;
                im2 = -1;

                for (int i = 1; i <= maxi; i++)
                {
                    int cost = source[im1] == target[jm1] ? 0 : 1;

                    int del = dCurrent[im1] + 1;
                    int ins = dMinus1[i] + 1;
                    int sub = dMinus1[im1] + cost;

                    //Fastest execution for min value of 3 integers
                    int min = (del > ins) ? (ins > sub ? sub : ins) : (del > sub ? sub : del);

                    if (i > 1 && j > 1 && source[im2] == target[jm1] && source[im1] == target[j - 2])
                    {
                        min = Math.Min(min, dMinus2[im2] + cost);
                    }

                    dCurrent[i] = min;
                    if (min < minDistance)
                    {
                        minDistance = min;
                    }
                    im1++;
                    im2++;
                }
                jm1++;
                if (minDistance > threshold)
                {
                    return int.MaxValue;
                }
            }

            int result = dCurrent[maxi];
            return (result > threshold) ? int.MaxValue : result;
        }
        
        private static void Swap<T>(ref T arg1, ref T arg2)
        {
            T temp = arg1;
            arg1 = arg2;
            arg2 = temp;
        }
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    .Include(t => t.AssociatedSite)
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
                    .Include(t => t.AssociatedSite)
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
            Console.WriteLine($"DEBUG: Request body contained: {transaction}");
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
                    
                    if (transaction.AssociatedSite != null)
                    {
                        Console.WriteLine($"DEBUG: Updating site");
                        // Site should not be editable from the transasction page, only selectable or newly created
                        var dbSite = db.Sites.FindAsync(transaction.AssociatedSite.SiteId).Result;
                        if (dbSite == null)
                        {                                                           
                            dbSite = new Domain.Entities.Site
                            {
                                Address = transaction.AssociatedSite.Address,
                                Description = transaction.AssociatedSite.Description,
                                Latitude = transaction.AssociatedSite.Latitude,
                                Longitude = transaction.AssociatedSite.Longitude,
                                CreatedOn = DateTime.UtcNow,
                                CreatedBy = updatedBy
                            };
                            db.Add(dbSite);
                        }
                        dbTransaction.AssociatedSite = dbSite;
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
    }
}

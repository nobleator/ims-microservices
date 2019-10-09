using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using transaction_service.Domain;
using transaction_service.Domain.Entities;
using transaction_service.Enums;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET api/products
        [HttpGet]
        public ActionResult<List<DTO.Product>> Get()
        {
            List<DTO.Product> productList;

            using (var db = new TransactionServiceDbContext())
            {
                productList = db.Products
                    .Where(p => !p.Deleted)
                    .OrderByDescending(p => p.UpdatedOn)
                    .Select(p => p.toDto())
                    .ToList();
                foreach (var product in productList)
                {
                    // get sale price
                    product.AvgSalePrice = Decimal.Round(db.LineItems
                        .Where(l => !l.Deleted && 
                                    l.ProductId == product.ProductId && 
                                    l.Quantity > 0 &&
                                    l.Transaction.TransactionType == TransactionTypeEnum.Sale.ToString())
                        .Sum(l => l.Price / l.Quantity), 2);
                    // get purchase price
                    product.AvgPurchasePrice = Decimal.Round(db.LineItems
                        .Where(l => !l.Deleted && 
                                    l.ProductId == product.ProductId && 
                                    l.Quantity > 0 &&
                                    l.Transaction.TransactionType == TransactionTypeEnum.Purchase.ToString())
                        .Sum(l => l.Price / l.Quantity), 2);
                }
            }

            return productList;
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public ActionResult<DTO.Product> Get(int id)
        {
            DTO.Product product;

            using (var db = new TransactionServiceDbContext())
            {
                product = db.Products
                    .Where(p => !p.Deleted &&
                                p.ProductId == id)
                    .Select(p => p.toDto())
                    .FirstOrDefault();

                if (product != null)
                {
                    // get sale price
                    product.AvgSalePrice = Decimal.Round(db.LineItems
                        .Where(l => !l.Deleted && 
                                    l.ProductId == product.ProductId && 
                                    l.Quantity > 0 &&
                                    l.Transaction.TransactionType == TransactionTypeEnum.Sale.ToString())
                        .Sum(l => l.Price / l.Quantity), 2);
                    // get purchase price
                    product.AvgPurchasePrice = Decimal.Round(db.LineItems
                        .Where(l => !l.Deleted && 
                                    l.ProductId == product.ProductId && 
                                    l.Quantity > 0 &&
                                    l.Transaction.TransactionType == TransactionTypeEnum.Purchase.ToString())
                        .Sum(l => l.Price / l.Quantity), 2);
                }
            }

            // Set default for new products
            if (product == null)
            {
                product = new DTO.Product
                {
                    ProductId = -1,
                    Name = "<name>",
                    Description = "<description>"
                };
            }

            return product;
        }

        // POST api/products
        [HttpPost]
        public void Post([FromBody] DTO.Product product)
        {
            Console.WriteLine($"DEBUG: Entering {nameof(Post)}");
            Console.WriteLine($"DEBUG: Request body contained: {product}");
            // TODO: updatedBy
            var updatedBy = "SYSTEM";
            using (var db = new TransactionServiceDbContext())
            {
                var dbProduct = db.Products.FindAsync(product.ProductId).Result;
                if (dbProduct == null)
                {
                    // PK is serial, so should be automatically generated
                    dbProduct = new Domain.Entities.Product
                    {
                        CreatedBy = updatedBy,
                        CreatedOn = DateTime.UtcNow
                    };
                    db.Add(dbProduct);
                }
                dbProduct.Name = product.Name;
                dbProduct.Description = product.Description;
                dbProduct.UpdatedBy = updatedBy;
                dbProduct.UpdatedOn = DateTime.UtcNow;
                db.SaveChangesAsync();
            }
        }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (var db = new TransactionServiceDbContext())
            {
                var dbProduct = db.Products.FindAsync(id).Result;
                if (dbProduct != null)
                {
                    dbProduct.Deleted = true;
                }
                db.SaveChangesAsync();
            }
        }
    }
}

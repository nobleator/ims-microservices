using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Domain.Entities
{
    [Table("line_item")]
    public class LineItem
    {
        [Column("line_item_id")]
        public int LineItemId { get; set; }
        [Column("transaction_id")]
        public int TransactionId { get; set; }
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("quantity")]
        public decimal Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }
        [Column("updated_on")]
        public DateTime UpdatedOn { get; set; }
        [Column("updated_by")]
        public string UpdatedBy { get; set; }
        [Column("created_on")]
        public DateTime CreatedOn { get; set; }
        [Column("created_by")]
        public string CreatedBy { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }

        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        public DTO.LineItem toDto()
        {
            return new DTO.LineItem
            {
                LineItemId = LineItemId,
                TransactionId = TransactionId,
                ProductId = ProductId,
                Quantity = Quantity,
                Price = Price
                // Don't include references here or you can trigger a stack overflow!
                // Transaction = Transaction.toDto(),
                // Product = Product.toDto()
            };
        }
    }
}
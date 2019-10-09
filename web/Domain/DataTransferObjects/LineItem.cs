using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Domain.DataTransferObjects
{
    public class LineItem
    {
        public int LineItemId { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        // Don't include references here or you can trigger a stack overflow! Only true for parent Transaction?
        // public Transaction Transaction { get; set; }
        // public Product Product { get; set; }
    }
}
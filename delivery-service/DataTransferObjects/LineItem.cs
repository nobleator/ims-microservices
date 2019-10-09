using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace delivery_service.Domain.DataTransferObjects
{
    public class LineItem
    {
        public int LineItemId { get; set; }
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
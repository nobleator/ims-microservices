using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Domain.DataTransferObjects
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        // The price properties are calculated from Transaction/Line Item prices
        public decimal AvgPurchasePrice { get; set; }
        public decimal AvgSalePrice { get; set; }
    }
}
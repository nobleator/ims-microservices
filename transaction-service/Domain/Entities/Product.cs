using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Domain.Entities
{
    [Table("product")]
    public class Product
    {
        [Column("product_id")]
        public int ProductId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("description")]
        public string Description { get; set; }
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

        public DTO.Product toDto()
        {
            return new DTO.Product
            {
                ProductId = ProductId,
                Name = Name,
                Description = Description
            };
        }
    }
}
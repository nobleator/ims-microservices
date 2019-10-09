using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Domain.Entities
{
    [Table("site")]
    public class Site
    {
        [Column("site_id")]
        public int SiteId { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("latitude")]
        public double Latitude { get; set; }
        [Column("longitude")]
        public double Longitude { get; set; }
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

        public DTO.Site toDto()
        {
            return new DTO.Site
            {
                SiteId = SiteId,
                Address = Address,
                Description = Description,
                Latitude = Latitude,
                Longitude = Longitude
            };
        }
    }
}
using transaction_service.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DTO = transaction_service.Domain.DataTransferObjects;

namespace transaction_service.Domain.Entities
{
    [Table("transaction")]
    public class Transaction
    {
        [Column("transaction_id")]
        public int TransactionId { get; set; }
        [Column("associated_client_id")]
        public int? ClientId { get; set; }
        [Column("transaction_type")]
        public string TransactionType { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("deliver_after")]
        public DateTime? DeliverAfter { get; set; }
        [Column("deliver_before")]
        public DateTime? DeliverBefore { get; set; }
        [Column("priority")]
        public int Priority { get; set; }

        [Column("site_name")]
        public string SiteName { get; set; }
        [Column("site_latitude")]
        public double SiteLatitude { get; set; }
        [Column("site_longitude")]
        public double SiteLongitude { get; set; }

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

        public List<LineItem> LineItems { get; set; }
        [ForeignKey("ClientId")]
        public Client AssociatedClient { get; set; }

        public DTO.Transaction toDto()
        {
            Enum.TryParse(TransactionType, out TransactionTypeEnum tte);
            return new DTO.Transaction
            {
                TransactionId = TransactionId,
                TransactionType = tte,
                Status = Status,
                DeliverBefore = DeliverBefore,
                DeliverAfter = DeliverAfter,
                SiteName = SiteName,
                SiteLatitude = SiteLatitude,
                SiteLongitude = SiteLongitude,
                LineItems = LineItems.Select(li => li.toDto()).ToList(),
                AssociatedClient = AssociatedClient?.toDto()
            };
        }
    }
}
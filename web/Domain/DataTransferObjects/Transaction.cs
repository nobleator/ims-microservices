using web.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Domain.DataTransferObjects
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime? DeliverAfter { get; set; }
        public DateTime? DeliverBefore { get; set; }
        public PriorityEnum Priority { get; set; }
        public string SiteName { get; set; }
        public double SiteLatitude { get; set; }
        public double SiteLongitude { get; set; }
        public string Status { get; set; }
        public List<LineItem> LineItems { get; set; }
        public Client AssociatedClient { get; set; }
    }
}
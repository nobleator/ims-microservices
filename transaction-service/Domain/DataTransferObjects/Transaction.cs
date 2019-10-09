using transaction_service.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace transaction_service.Domain.DataTransferObjects
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public string Status { get; set; }
        public DateTime DeliverBefore { get; set; }
        public DateTime DeliverAfter { get; set; }
        public List<LineItem> LineItems { get; set; }
        public Site AssociatedSite { get; set; }
        public Client AssociatedClient { get; set; }
    }
}
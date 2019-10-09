using web.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Domain.DataTransferObjects
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public string Status { get; set; }
        public List<LineItem> LineItems { get; set; }
        public Site AssociatedSite { get; set; }
        public Client AssociatedClient { get; set; }
    }
}
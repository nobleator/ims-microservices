using transaction_service.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace transaction_service.Domain.DataTransferObjects
{
    // Note: This does not have a matching DB entity because the fields are inherent to each transaction.
    // This class is used for searching for similar sites, but not referencing the same underlying entity.
    public class SiteSearchResult
    {
        public string SiteName { get; set; }
        public double SiteLatitude { get; set; }
        public double SiteLongitude { get; set; }
    }
}
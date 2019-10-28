using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace delivery_service.Domain.DataTransferObjects
{
    public class CandidateDelivery
    {
        public int LineItemId { get; set; }
        public int Priority { get; set; }
        public decimal Profit { get; set; }
        public decimal SiteLatitude { get; set; }
        public decimal SiteLongitude { get; set; }
    }
}
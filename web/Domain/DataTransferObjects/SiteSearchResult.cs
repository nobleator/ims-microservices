using web.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace web.Domain.DataTransferObjects
{
    public class SiteSearchResult
    {
        public string SiteName { get; set; }
        public double SiteLatitude { get; set; }
        public double SiteLongitude { get; set; }
    }
}
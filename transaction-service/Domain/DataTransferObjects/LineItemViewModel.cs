using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace transaction_service.Domain.DataTransferObjects
{
    public class LineItemViewModel
    {
        public LineItem LineItem { get; set; }
        public List<SelectListItem> SelectableProducts { get; set; }
    }
}
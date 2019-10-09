using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Domain.DataTransferObjects
{
    public class FormField
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public bool Editable { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
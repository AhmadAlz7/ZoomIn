using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Accessloger
    {
        public decimal AccessId { get; set; }
        public string AccessPage { get; set; }
        public decimal? AccessCounter { get; set; }
        public DateTime? AccessDate { get; set; }
        public string AccessVisitor { get; set; }
    }
}

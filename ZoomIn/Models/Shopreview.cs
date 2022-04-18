using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Shopreview
    {
        public decimal ReviewId { get; set; }
        public decimal? Rate { get; set; }
        public string Reviewcomment { get; set; }
        public decimal? ReviewerId { get; set; }
        public DateTime? ReviewDate { get; set; }

        public virtual Enduser Reviewer { get; set; }
    }
}

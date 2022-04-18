using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Loginloger
    {
        public decimal LoginId { get; set; }
        public decimal? LoginCounter { get; set; }
        public DateTime? LoginDate { get; set; }
        public decimal? LoginUserId { get; set; }

        public virtual Enduser LoginUser { get; set; }
    }
}

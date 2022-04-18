using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Usertransaction
    {
        public decimal UtransactionId { get; set; }
        public DateTime? Utransactiondate { get; set; }
        public decimal? Paymenttotal { get; set; }
        public decimal? Relatedsiterate { get; set; }
        public decimal? Utransactionvalue { get; set; }
        public string Utdescription { get; set; }
        public decimal? PurchaseId { get; set; }
        public decimal? FromId { get; set; }
        public decimal? ToId { get; set; }

        public virtual Enduser From { get; set; }
        public virtual Userpurchaseitem Purchase { get; set; }
        public virtual Enduser To { get; set; }
    }
}

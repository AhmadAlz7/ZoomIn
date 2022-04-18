using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Balancetransaction
    {
        public decimal BtransactionId { get; set; }
        public DateTime? Btransactiondate { get; set; }
        public decimal? Paymenttotal { get; set; }
        public decimal? Relatedsiterate { get; set; }
        public decimal? Btransactionvalue { get; set; }
        public string Btdescription { get; set; }
        public decimal? PurchaseId { get; set; }
        public decimal? FromId { get; set; }
        public string Balancelock { get; set; }

        public virtual Balance BalancelockNavigation { get; set; }
        public virtual Enduser From { get; set; }
        public virtual Userpurchaseitem Purchase { get; set; }
    }
}

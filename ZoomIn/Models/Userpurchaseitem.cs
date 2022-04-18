using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Userpurchaseitem
    {
        public decimal PurchaseId { get; set; }
        public DateTime? Purchasedate { get; set; }
        public decimal? Paymenttotal { get; set; }
        public decimal? Relatedsiterate { get; set; }
        public decimal? BuyerId { get; set; }
        public decimal? ItemId { get; set; }

        public virtual Enduser Buyer { get; set; }
        public virtual Item Item { get; set; }
        public virtual Balancetransaction Balancetransaction { get; set; }
        public virtual Usertransaction Usertransaction { get; set; }
    }
}

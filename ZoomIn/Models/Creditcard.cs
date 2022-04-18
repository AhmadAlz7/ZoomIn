using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Creditcard
    {
        public Creditcard()
        {
            Endusers = new HashSet<Enduser>();
        }

        public decimal CardId { get; set; }
        public decimal Cardnumber { get; set; }
        public decimal Cardkey { get; set; }
        public DateTime Expirydate { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<Enduser> Endusers { get; set; }
    }
}

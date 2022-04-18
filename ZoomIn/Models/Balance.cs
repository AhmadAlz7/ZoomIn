using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Balance
    {
        public Balance()
        {
            Balancetransactions = new HashSet<Balancetransaction>();
        }

        public string BalanceLock { get; set; }
        public decimal? Profitrate { get; set; }
        public decimal? BalanceValue { get; set; }

        public virtual ICollection<Balancetransaction> Balancetransactions { get; set; }
    }
}

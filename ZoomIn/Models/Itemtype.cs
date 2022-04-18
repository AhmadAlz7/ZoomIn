using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Itemtype
    {
        public Itemtype()
        {
            Items = new HashSet<Item>();
        }

        public decimal TypeId { get; set; }
        public string Typename { get; set; }
        public string Typedescription { get; set; }
        public DateTime Createdate { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

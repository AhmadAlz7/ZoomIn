using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Itemcategory
    {
        public Itemcategory()
        {
            Items = new HashSet<Item>();
        }

        public decimal CateId { get; set; }
        public string Catename { get; set; }
        public DateTime Createdate { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}

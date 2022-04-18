using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Item
    {
        public Item()
        {
            Userlikeitems = new HashSet<Userlikeitem>();
            Userpurchaseitems = new HashSet<Userpurchaseitem>();
        }

        public decimal ItemId { get; set; }
        public byte[] ItemInnerFile { get; set; }

        public string Itemname { get; set; }
        public DateTime? Uploaddate { get; set; }
        public DateTime? Createdate { get; set; }
        public decimal? Width { get; set; }
        public decimal? Height { get; set; }
        public decimal? Price { get; set; }
        public string Itemdescription { get; set; }
        public string Takenlocation { get; set; }
        public decimal Noviews { get; set; }
        public decimal? TypeId { get; set; }
        public decimal? CategoryId { get; set; }
        public decimal OwnerId { get; set; }


        public decimal Nolikes { get; set; }
        public decimal Nopurchases { get; set; }
        public decimal Popularity { get; set; }

        [NotMapped]
        public virtual IFormFile ItemFile { get; set; }
        public string ItemExtension { get; set; }

        /*[NotMapped]
        public virtual double Popularity { get; set; }*/

        public virtual Itemcategory Category { get; set; }
        public virtual Enduser Owner { get; set; }
        public virtual Itemtype Type { get; set; }
        public virtual ICollection<Userlikeitem> Userlikeitems { get; set; }
        public virtual ICollection<Userpurchaseitem> Userpurchaseitems { get; set; }
    }
}

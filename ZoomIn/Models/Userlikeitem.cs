using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Userlikeitem
    {
        public decimal LikeId { get; set; }
        public DateTime? Likedate { get; set; }
        public decimal? UserId { get; set; }
        public decimal? ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Enduser User { get; set; }
    }
}

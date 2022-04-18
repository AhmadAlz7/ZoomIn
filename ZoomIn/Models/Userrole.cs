using System;
using System.Collections.Generic;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Userrole
    {
        public Userrole()
        {
            Endusers = new HashSet<Enduser>();
        }

        public decimal RoleId { get; set; }
        public decimal? Roleindex { get; set; }
        public string Rolename { get; set; }
        public string Roledescription { get; set; }

        public virtual ICollection<Enduser> Endusers { get; set; }
    }
}

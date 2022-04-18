using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#nullable disable

namespace ZoomIn.Models
{
    public partial class Enduser
    {
        public Enduser()
        {
            Balancetransactions = new HashSet<Balancetransaction>();
            Items = new HashSet<Item>();
            Loginlogers = new HashSet<Loginloger>();
            Userlikeitems = new HashSet<Userlikeitem>();
            Userpurchaseitems = new HashSet<Userpurchaseitem>();
            UsertransactionFroms = new HashSet<Usertransaction>();
            UsertransactionTos = new HashSet<Usertransaction>();
        }

        public decimal UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public byte[] UserImage { get; set; }
        public DateTime? Registerdate { get; set; }

        //[Remote("IsUserNameEsist","LoginRegister", ErrorMessage = "UserName is already taken!")]
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }

        [NotMapped]
        [Compare("UserPassword")]
        public string ConfirmPassword { get; set; }


        //[Remote("IsEmailEsist", "LoginRegister", ErrorMessage = "Email is already taken!")]
        public string UserEmail { get; set; }
        public decimal? RoleId { get; set; }
        public decimal? CreditcId { get; set; }

        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public string ImageExtension { get; set; }

        public virtual Creditcard Creditc { get; set; }
        public virtual Userrole Role { get; set; }
        public virtual Shopreview Shopreview { get; set; }
        public virtual ICollection<Balancetransaction> Balancetransactions { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Loginloger> Loginlogers { get; set; }
        public virtual ICollection<Userlikeitem> Userlikeitems { get; set; }
        public virtual ICollection<Userpurchaseitem> Userpurchaseitems { get; set; }
        public virtual ICollection<Usertransaction> UsertransactionFroms { get; set; }
        public virtual ICollection<Usertransaction> UsertransactionTos { get; set; }
    }
}

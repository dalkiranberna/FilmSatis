using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFilmSatis.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Customer : IdentityUser
    {
		public long TC { get; set; }
		public string NameSurname { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
		public virtual ShoppingCart ShoppingCart { get; set; }
		public virtual List<Order> Orders { get; set; }

    }
    public class Employee: Customer
    {
       public string ReportsToEmail { get; set; }

    }
}
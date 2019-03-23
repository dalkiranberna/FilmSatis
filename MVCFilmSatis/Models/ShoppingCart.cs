using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFilmSatis.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartID { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual Customer Customer { get; set; }
		public virtual List<Movies> Movies { get; set; }
        public decimal? SubTotal {
            get
            {
                return Movies.Sum(x => x.Price);
            }
        }

        public ShoppingCart()
        {
            CreateDate = DateTime.Now;
        }
    }
}
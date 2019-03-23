using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCFilmSatis.Models
{
    public class Movies
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Runtime { get; set; }
        public int OscarNominations { get; set; }
        public int OscarWins { get; set; }
        public decimal Price { get; set; }
		public string YoutubeId { get; set; }
        public virtual List<ShoppingCart> ShoppingCarts { get; set; }
		public virtual List<OrderItem> OrderItems { get; set; }
    }
}
using Microsoft.AspNet.Identity;
using MVCFilmSatis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilmSatis.Controllers
{
	public class CartController : Controller
	{
		ApplicationDbContext db = new ApplicationDbContext();
		
		public ActionResult Index(string id)
		{
			if (User.Identity.IsAuthenticated)
			{
				string uId = User.Identity.GetUserId();
				Customer c = db.Users.Find(uId);

				if (c.ShoppingCart == null)
				{
					c.ShoppingCart = new ShoppingCart();
					c.ShoppingCart.Movies = new List<Movies>();
				}
				return View(c.ShoppingCart);
			}
			return RedirectToAction("Index", "Home");

		}
        
		public ActionResult Checkout()
		{
			string uId = User.Identity.GetUserId();
			Customer c = db.Users.Find(uId);

			ViewBag.Total = c.ShoppingCart.SubTotal;
			ViewBag.CardNo = c.ShoppingCart.ShoppingCartID;
			return View();
		}

		[HttpPost]
		public ActionResult PayBankTransfer(int? approve)
		{
			if (approve.HasValue && approve.Value == 1)
			{
				BankTransferPayment p1 = new BankTransferPayment();
				p1.IsApproved = false;
				p1.NameSurname = User.Identity.GetNameSurname();
				p1.TC = User.Identity.GetTC();

				BankTransferService service = new BankTransferService();
				bool isPaid = service.MakePayment(p1);

				if (isPaid)
				{
					CreateOrder(isPaid);
					ResetShoppingCard();
				}
					
				return RedirectToAction("Index", "Home");
			}

			return RedirectToAction("Checkout");
		}

		private void ResetShoppingCard()
		{//alışveriş tamamlanınca kişinin sepeti sıfırlanacak
			string uId = User.Identity.GetUserId();
			Customer c = db.Users.Find(uId);
			c.ShoppingCart.Movies.Clear();
			db.Entry(c).State = EntityState.Modified;
			db.SaveChanges();
		}

		public Order CreateOrder(bool isPaid)
		{
			string uId = User.Identity.GetUserId();
			Customer c = db.Users.Find(uId);
			Order order = new Order();
			order.Customer = c;
			order.IsPaid = isPaid;
			order.OrderItems = new List<OrderItem>();
			foreach (var item in c.ShoppingCart.Movies)
			{
				OrderItem oi = new OrderItem();
				order.Date = DateTime.Now;
				oi.Movies = item;
				oi.Count = 1;
				oi.Price = item.Price;
				order.OrderItems.Add(oi);
			}
			order.SubTotal = c.ShoppingCart.SubTotal.Value;
			db.Orders.Add(order);
			db.SaveChanges(); 

			return order;
		}

		public ActionResult Delete(int id)
		{
			Movies toBeDeleted = db.Movies.Find(id);
            
			string uId = User.Identity.GetUserId(); 
			Customer c = db.Users.Find(uId);
			c.ShoppingCart.Movies.Remove(toBeDeleted);
			db.Entry(c).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult AddToCart(int id)
		{
			if (!User.Identity.IsAuthenticated)
				return RedirectToAction("Index", "Home", new { error = "Login to buy movies."});

			string uId = User.Identity.GetUserId();
			Customer c = db.Users.Find(uId);
            
			if (c.ShoppingCart == null)
				c.ShoppingCart = new ShoppingCart();

			if (c.ShoppingCart.Movies == null)
				c.ShoppingCart.Movies = new List<Movies>();

			if (c.ShoppingCart.Movies.Any(x => x.MovieId == id))
			{
				return RedirectToAction("Index", "Home", new { error = "You already have this movie in your cart."});
			}
			else
			{
				Movies chosenMovie = db.Movies.Find(id);
				c.ShoppingCart.Movies.Add(chosenMovie);

				db.Entry(c).State = EntityState.Modified;
				db.SaveChanges();

				return RedirectToAction("Index", "Home");
			}


		}
	}
}
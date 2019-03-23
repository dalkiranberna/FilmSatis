using Microsoft.AspNet.Identity;
using MVCFilmSatis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilmSatis.Controllers
{
	[Authorize]
    public class PersonalController : Controller
    {
		ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult MyOrders()
		{
			string uId = User.Identity.GetUserId();
			Customer c = db.Users.Find(uId);

			return View(c.Orders);
		}
    }
}
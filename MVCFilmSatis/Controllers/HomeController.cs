using Microsoft.AspNet.Identity;
using MVCFilmSatis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilmSatis.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext Db = new ApplicationDbContext();
        
        public ActionResult Index(int? page,string error)
        {
            ViewBag.Error = error;
            if (User.Identity.IsAuthenticated)//kişi giriş yaptıysa
            {
                string uid = User.Identity.GetUserId();
                Customer loggedInUser = Db.Users.Find(uid);
                if (loggedInUser.ShoppingCart == null)
                {
                    ViewBag.CartMovieCount = 0;
                }
                else
                {
                    var l = loggedInUser.ShoppingCart.Movies;
                    var c = l == null ? 0 : l.Count;
                    ViewBag.CartMovieCount = c;
                }

            }
            List<Movies> List = new List<Movies>();
           
            if (page.HasValue)//sayfa seçildiyse 
            {
                int a = (page.Value - 1) * Settings.moviePerPage;
                List = Db.Movies
                    .OrderBy(x => x.MovieId)
                    .Skip(a)
                    .Take(Settings.moviePerPage)
                    .ToList();
            }else
            {
                List = Db.Movies.Take(Settings.moviePerPage).ToList();
               
            }
            float MovieCount = Db.Movies.Count();
            double PageCount =Math.Ceiling(MovieCount / Settings.moviePerPage);
            ViewBag.filmCount = List.Count();
            int current = page.HasValue ? page.Value : 1;//kaçıncı sayfadayız?

            ViewBag.Start = current > 2 ? current - 2 : 1;
            ViewBag.End = current < PageCount - 2 ? current + 2 : PageCount;
            ViewBag.CurrentPage = current;
            ViewBag.PrevVisible = current > 1;
            ViewBag.NextVisible = current < PageCount;

			HomeViewModel hvm = new HomeViewModel();
			hvm.Movies = List;
			hvm.Sliders = Db.Sliders.ToList();

            return View(hvm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
     
     
    }
}
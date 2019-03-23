using MVCFilmSatis.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVCFilmSatis.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class AdminController : Controller
	{
		ApplicationDbContext db = new ApplicationDbContext();

		public ActionResult Index()
		{
			return View();
		}
		
		public ActionResult Slider()
		{
			return View(db.Sliders.ToList());
		}

		public ActionResult SliderCreate()
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult SliderCreate(HttpPostedFileBase imageFile)
		{
			if (imageFile != null && imageFile.ContentLength != 0)
			{
				string filePath = Server.MapPath("/Uploads/Sliders/"); //kaydedilecek yer
				string thumbPath = filePath + "Thumb/";
				string largePath = filePath + "Large/";

				imageFile.SaveAs(largePath + imageFile.FileName);

				Image i = Image.FromFile(largePath + imageFile.FileName);
				Size s = new Size(380, 100);

				Image small = Helper.ResizeImage(i, s);
				small.Save(thumbPath + imageFile.FileName);

				i.Dispose();

				MVCFilmSatis.Models.Slider slider = new Slider();
				slider.LargeImageURL = "/Uploads/Sliders/Large/" + imageFile.FileName;
				slider.ThumbnailURL = "/Uploads/Sliders/Thumb/" + imageFile.FileName;

				db.Sliders.Add(slider);
				db.SaveChanges();
				return RedirectToAction("Slider");
			}
			return View();
		}

		public ActionResult DeleteSlider(int id)
		{
			Slider s = db.Sliders.Find(id);
			//ana dizinin absolute pathi:
			var path = Server.MapPath("/");
			var lg = path + s.LargeImageURL;
			var sm = path + s.ThumbnailURL;

			System.IO.File.Delete(lg);
			System.IO.File.Delete(sm);

			db.Sliders.Remove(s);
			db.SaveChanges();

			return RedirectToAction("Slider");
		}
	}
}
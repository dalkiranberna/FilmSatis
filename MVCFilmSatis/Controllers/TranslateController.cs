using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVCFilmSatis.Controllers
{
    public class TranslateController : Controller
    {
        // GET: Translate
		//?id=tr
		//Translate/Index/en
		//{controller}/{action}/{id}
        public ActionResult Index(string id, string url)
        {
			HttpCookie langCookie = new HttpCookie("lang");
			langCookie.Value = id;
			Response.Cookies.Add(langCookie);

			return Redirect(url);
        }
    }
}
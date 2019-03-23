using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilmSatis.Controllers
{
	public class OnlyYoungAndAdmin : ActionFilterAttribute, IActionFilter
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			bool isYoung = filterContext.HttpContext.User.Identity.IsYoung();
			bool isAdmin = filterContext.HttpContext.User.IsInRole("Administrator");

			if (isYoung || isAdmin)
				OnActionExecuting(filterContext);
			else
				filterContext.Result = new RedirectResult("/");
		}
	}
}
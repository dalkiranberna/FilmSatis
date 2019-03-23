using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCFilmSatis
{
    public class MvcApplication : System.Web.HttpApplication
    {
		protected void Application_BeginRequest() //bunu biz yazdık ve ismi böyle olmak zorunda. global.asax methods diye aratarak bulabiliriz. Bu metotta istek gelince yapılacaklar yazılır:
		{
			var lang = Request.Cookies["lang"]; //cookiemizin adı lang olduğu için lang yazmamız gerekir. kişi yeni gelmişse lang null de olabilir
			if (lang != null)
			{
				string language = lang.Value;
				//CultureInfo("tr")
				//CultureInfo("tr-TR")
				//CultureInfo("en-US")
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
			}
		}

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

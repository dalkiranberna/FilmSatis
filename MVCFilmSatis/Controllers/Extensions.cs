using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MVCFilmSatis.Controllers
{
	public static class Extensions
	{
		public static string GetTC(this IIdentity id) 
		{
			var allId = (ClaimsIdentity)id;
			return allId.FindFirst("TC").Value;
		}

		public static string GetNameSurname(this IIdentity id)
		{
			var allId = (ClaimsIdentity)id;
			return allId.FindFirst("NameLastname").Value;

		}

		public static bool IsYoung(this IIdentity id)
		{
			try
			{
				var allId = (ClaimsIdentity)id;
				string ageGroup = allId.FindFirst("AgeGroup").Value;
				return ageGroup == "Young";
			}
			catch
			{
				return false;
			}
		}
	}
}
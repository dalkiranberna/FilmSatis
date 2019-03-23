using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MVCFilmSatis.Controllers
{
	public static class Helper
	{

		public static Image ResizeImage(Image imgToResize, Size size)
		{
			return new Bitmap(imgToResize, size);
		}
	}
}
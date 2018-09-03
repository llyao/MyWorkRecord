using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientCore.DB;

namespace WebApp.Controllers
{
	public class HomeController : ProjectCenterBaseController
    {
		public ActionResult Index()
		{
			SetProjectVersionData();
			return View();
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
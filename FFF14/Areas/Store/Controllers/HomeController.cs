using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FFF.Areas.Store.Controllers
{
	[RequireHttps]
	[AllowAnonymous]
	public class HomeController : FFF.Controllers.MainController
	{
		public override ActionResult Index()
		{
			return View();
		}
		public PartialViewResult Sidebar()
		{
			return PartialView("_Sidebar");
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
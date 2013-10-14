using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Areas.Account.Controllers
{
	public class SettingsController : FFF.Controllers.MainController
    {
		[HttpPost]
		[ValidateAntiForgeryToken]
		public override ActionResult Index()
		{
			return PartialView( "_Settings", this.Account );
		}
	}
}
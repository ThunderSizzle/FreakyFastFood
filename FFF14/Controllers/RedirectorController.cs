using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Controllers
{
	[RequireHttps]
	[AllowAnonymous]
    public class RedirectorController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
			return RedirectToRoutePermanent("Store_default", new { controller = "" } );
            //return RedirectToAction("Index", "Home", new { area = "Store" });
        }
	}
}
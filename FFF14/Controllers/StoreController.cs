using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Controllers
{
	[AllowAnonymous]
	[RequireHttps]
    public class StoreController : Controller
    {
        //
        // GET: /Store/
        public ActionResult Index()
        {
            return View();
        }
	}
}
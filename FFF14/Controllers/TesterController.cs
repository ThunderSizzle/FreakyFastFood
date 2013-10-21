using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Controllers
{
	[Authorize]
	[RequireHttps]
    public class TesterController : Controller
    {
        //
        // GET: /Tester/
        public ActionResult Index()
        {
            return View();
        }
	}
}
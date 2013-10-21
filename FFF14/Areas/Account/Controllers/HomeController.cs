using FFF.Controllers;
using FFF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FFF.Areas.Account.Controllers
{
	[RequireHttps]
	[Authorize]
	/// <summary>
	/// This Controller acts as a base for all User-Specific Controllers. 
	/// </summary>
	public class HomeController : MainController
	{
		protected override void Dispose( bool disposing )
		{
			db.Dispose();
			base.Dispose( disposing );
		}
		public ActionResult Index()
		{
			return View();
		}
		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToAction("Index", "Store");
			}
		}
	}
}
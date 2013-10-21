using FFF.Models;
using FFF.ViewModels;
using FFF.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FFF.Areas.Account.Controllers
{
	[RequireHttps]
	[ValidateAntiForgeryToken]
	public class ProfileController : FFF.Controllers.MainController
    {
		[HttpGet]
		public PartialViewResult Index()
		{
			return PartialView( "_Profile" );
		}
		[HttpGet]
		public PartialViewResult Edit()
		{
			return PartialView( "_EditProfile" );
		}
		[HttpGet]
		public PartialViewResult Delete()
		{
			return PartialView( "_DeleteProfile" );
		}
	}
}
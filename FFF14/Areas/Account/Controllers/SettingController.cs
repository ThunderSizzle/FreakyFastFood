using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Areas.Account.Controllers
{
	public class SettingController : FFF.Controllers.MainController
    {
		[HttpGet]
		public PartialViewResult Index()
		{
			return PartialView( "_Settings" );
		}
		[HttpGet]
		public PartialViewResult Details()
		{
			return PartialView( "_DetailsSetting" );
		}
		[HttpGet]
		public PartialViewResult Create()
		{
			return PartialView( "_CreateSetting" );
		}
		[HttpGet]
		public PartialViewResult Edit()
		{
			return PartialView( "_EditSetting" );
		}
		[HttpGet]
		public PartialViewResult Delete()
		{
			return PartialView( "_DeleteSetting" );
		}
	}
}
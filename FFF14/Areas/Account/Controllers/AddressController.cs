using FFF.Hubs;
using FFF.InputModels;
using FFF.Models;
using FFF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FFF.Areas.Account.Controllers
{
	[RequireHttps]
	[Authorize]
	public class AddressController : FFF.Controllers.MainController
    {
		[HttpGet]
		public PartialViewResult Index()
		{
			return PartialView( "_Addresses" );
		}
		[HttpGet]
		public PartialViewResult Create()
		{
			return PartialView( "_CreateAddress" );
		}
		[HttpGet]
		public PartialViewResult Edit()
		{
			return PartialView( "_EditAddress" );
		}
		[HttpGet]
		public PartialViewResult Delete()
		{
			return PartialView( "_DeleteAddress" );
		}
	}
}
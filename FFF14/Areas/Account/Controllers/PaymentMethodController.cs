using FFF.InputModels;
using FFF.Models;
using FFF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Areas.Account.Controllers
{
	[RequireHttps]
	[Authorize]
	public class PaymentMethodController : FFF.Controllers.MainController
	{
		[HttpGet]
		public PartialViewResult Index()
		{
			return PartialView( "_PaymentMethods" );
		}
		[HttpGet]
		public PartialViewResult Details()
		{
			return PartialView( "_DetailsPaymentMethod" );
		}
		[HttpGet]
		public PartialViewResult Create()
		{
			return PartialView( "_CreatePaymentMethod" );
		}
		[HttpGet]
		public PartialViewResult Edit()
		{
			return PartialView( "_EditPaymentMethod" );
		}
		[HttpGet]
		public PartialViewResult Delete()
		{
			return PartialView( "_DeletePaymentMethod" );
		}
	}
}
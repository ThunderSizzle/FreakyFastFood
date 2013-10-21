using FFF.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FFF.Areas.Account.Controllers
{
	/// <summary>
	/// This Controller handles all Order Actions for a User Account, such as creating a new order, or viewing past orders.
	/// </summary>
	[RequireHttps]
	[Authorize]
	public class OrderController : FFF.Controllers.MainController
    {
        //
		// GET: /Order/
		[HttpGet]
		public PartialViewResult Index()
        {
            return PartialView("_Orders");
        }

        //
        // GET: /Order/Details/5
		[HttpGet]
		public PartialViewResult Details( )
        {
			return PartialView( "_DetailsOrder" );
        }
		[HttpGet]
		public PartialViewResult Create( )
		{
			return PartialView( "_CreateOrder" );
		}
		[HttpGet]
		public PartialViewResult Edit( )
		{
			return PartialView( "_EditOrder" );
		}
		[HttpGet]
		public PartialViewResult Delete( )
		{
			return PartialView( "_DeleteOrder" );
		}
    }
}

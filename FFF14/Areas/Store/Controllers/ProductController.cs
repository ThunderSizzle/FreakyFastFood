using FFF.Models.ItemSystem;
using FFF.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Areas.Store.Controllers
{
	[RequireHttps]
	[Authorize]
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        public PartialViewResult Create(Guid id)
        {
	//		ProductInput model = new ProductInput( db.Items.Find( id ) );
            return PartialView();
        }
		public PartialViewResult CreateConfirmed()
		{
			return PartialView();
		}
	}
}
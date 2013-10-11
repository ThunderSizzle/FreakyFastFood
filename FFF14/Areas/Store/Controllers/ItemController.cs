using FFF.Models.ItemSystem;
using FFF.ViewModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Areas.Store.Controllers
{
	[RequireHttps]
	[Authorize]
	public class ItemController : Controller
    {
	/*
        //
        // GET: /Item/
		//[ChildActionOnly]
        public ActionResult Index()
        {
            return View(db.Items.ToList());
        }

		public ActionResult Details( Guid id )
		{
			if ( db.Items.Any( c => c.RID == id ) )
			{
				Item Item = db.Items.First( c => c.RID == id );
				ItemView model = new ItemView()
				{
					ItemID = id,
					Description = Item.Description,
					Price = Item.Price,
					Title= Item.Title
				};

				return PartialView( model );
			}			

			return HttpNotFound();
		}
	*/
	}
}
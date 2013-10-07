using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFF.Models.OrderSystem;
using FFF.Models;
using FFF.Models.ProfileSystem;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using FFF.Models.UserSystem;
using FFF.ViewModels.Item;
using FFF.Models.ItemSystem;


namespace FFF.Controllers
{
	[RequireHttps]
	[AllowAnonymous]
	/// <summary>
	/// This Controller handles all Order Actions for a User Account, such as creating a new order, or viewing past orders.
	/// </summary>
    public class ShoppingCartController : MainController
    {
		protected override void OnResultExecuted( ResultExecutedContext filterContext )
		{
			var name = filterContext.HttpContext.User.Identity.Name;
			if ( filterContext.HttpContext.Session["Account"] != null )
				filterContext.HttpContext.Session["Account"] = this.Account;
			base.OnResultExecuted( filterContext );
		}

		// GET: /Order/
		[HttpPost]
		[ValidateAntiForgeryToken]
		public PartialViewResult Index()
        {
			return PartialView("_ShoppingCartModal", Account.ShoppingCart);
        }

		[ChildActionOnly]
		public PartialViewResult Navbar()
		{
			return PartialView( "_ShoppingCartNavbar", Account.ShoppingCart );
		}

		/*
		[HttpPost]
		public ActionResult AddProduct(ProductCreateModel model)
		{
			if ( db.Items.Any( c => c.RID == model.ItemID ) )
			{
				Item Item = db.Items.FirstOrDefault( c => c.RID == model.ItemID );
				Product Product = new Product(Item, model.SelectedOptions);

				this.Account.ShoppingCart.Products.Add( Product );
				db.SaveChanges();
				return View( "Index" );
			}
			return HttpNotFound();
		}
		*/
		[HttpPost]
		public ActionResult RemoveProduct( Guid id )
		{
			if ( this.Account.ShoppingCart.Products.Any( c => c.RID == id ) )
			{
				this.Account.ShoppingCart.Products.Remove( Account.ShoppingCart.Products.First( c => c.RID == id ) );
				db.SaveChanges();
				return View( "Index" );
			}
			return HttpNotFound();
		}

		[HttpPost]
		public ActionResult SelectAddress (Guid id)
		{
			if ( this.Account.Addresses.Any( c => c.RID == id ) )
			{
				this.Account.ShoppingCart.DeliveryAddress = this.Account.Addresses.First( c => c.RID == id );
				db.SaveChanges();
				return View ("Index");
			}
			return HttpNotFound();
		}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

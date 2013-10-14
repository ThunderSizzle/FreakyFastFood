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
		public override ActionResult Index()
        {
            return PartialView("_Orders", Account.Orders.ToList());
        }

        //
        // GET: /Order/Details/5
		public PartialViewResult Details( Guid id )
        {
			if(Account.Orders.Any(c => c.RID == id))
			{
				return PartialView( Account.Orders.First( c => c.RID == id ) );
			}
			return PartialView();
        }

        //
        // POST: /Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Migrate()
        {
			if ( Account.ShoppingCart.Products.Count == 0 )
			{	 
				return View( "Index" , "ShoppingCart");
			}
			else
			{
				Order Order = new Order();
				Order.Products = Account.ShoppingCart.Products;
				Order.Status = "Order Migrated";
				Account.Orders.Add( Order );
                db.SaveChanges();
				return View( "SelectAddress", Order );
            }
        }

		[HttpPost]
		public ActionResult SelectAddress(Guid OrderID, Guid AddressID)
		{
			if(this.Account.Orders.Any(c => c.RID == OrderID))
			{
				if ( this.Account.Addresses.Any( c => c.RID == AddressID ) )
				{
					this.Account.Orders.First( c => c.RID == OrderID ).Status = "Address Verified";
					this.Account.Orders.First( c => c.RID == OrderID ).DeliveryAddress = this.Account.Addresses.First( c => c.RID == AddressID );
					db.SaveChanges();
					return View("SelectPayment", this.Account.Orders.First(c => c.RID == OrderID));
				}
			}
			return HttpNotFound();
		}
		[HttpPost]
		public ActionResult SelectPayment( Guid OrderID, Guid PaymentID )
		{
			if(this.Account.Orders.Any(c => c.RID == OrderID))
			{
				if ( this.Account.PaymentMethods.Any( c => c.RID == PaymentID ) )
				{
					this.Account.Orders.First( c => c.RID == OrderID ).Status = "Payment Verified";
					this.Account.Orders.First( c => c.RID == OrderID ).PaymentMethod = this.Account.PaymentMethods.First( c => c.RID == PaymentID );
					db.SaveChanges();
					return View("VerifyOrder", this.Account.Orders.First(c => c.RID == OrderID));
				}
			}
			return HttpNotFound();
		}
		[HttpPost]
		public ActionResult VerifyOrder( Guid OrderID )
		{
			if(this.Account.Orders.Any(c => c.RID == OrderID))
			{
				this.Account.Orders.First( c => c.RID == OrderID ).Status = "Submitted";
				this.Account.ShoppingCart.Products.Clear();
				db.SaveChanges();
				return View("Details", this.Account.Orders.First(c => c.RID == OrderID));
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

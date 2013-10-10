using FFF.Models;
using FFF.Models.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using FFF.Models.OrderSystem;
using FFF.Models.ItemSystem;

namespace FFF.Areas.Account.Controllers
{
	[RequireHttps]
	[Authorize]
	/// <summary>
	/// This Controller acts as a base for all User-Specific Controllers. 
	/// </summary>
	public class AccountController : Controller
	{
		protected DatabaseContext db;
		protected FFF.Models.UserSystem.Account Account;
		protected AuthenticationIdentityManager IdentityManager;
		protected Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }

		public AccountController()
		{
			db = new DatabaseContext();
			IdentityManager = new AuthenticationIdentityManager( new IdentityStore( db ) );
		}
		protected override void Initialize( System.Web.Routing.RequestContext requestContext )
		{
			String name = requestContext.HttpContext.User.Identity.Name;
			if ( db.Accounts.Any( c => c.User.UserName == name ) )
			{
				this.Account = db.Accounts.First( c => c.User.UserName == name );
				if ( requestContext.HttpContext.Session["Account"] != null )
				{
					FFF.Models.UserSystem.Account temp = requestContext.HttpContext.Session["Account"] as FFF.Models.UserSystem.Account;
					requestContext.HttpContext.Session["Account"] = null;

					foreach ( var product in temp.ShoppingCart.Products )
					{
						product.Item = db.Items.Find( product.Item.RID );
						product.ShoppingCart = this.Account.ShoppingCart;
						db.SaveChanges();
						db.Products.Add( product );
						db.SaveChanges();
					}
				}
			}
			else if ( requestContext.HttpContext.Session["Account"] == null )
			{
				this.Account = new FFF.Models.UserSystem.Account();
				this.Account.ShoppingCart = new ShoppingCart();
				requestContext.HttpContext.Session["Account"] = this.Account;
			}
			else
			{
				this.Account = requestContext.HttpContext.Session["Account"] as FFF.Models.UserSystem.Account;
			}
			base.Initialize( requestContext );
		}
		protected override void OnActionExecuting( ActionExecutingContext filterContext )
		{

			base.OnActionExecuting( filterContext );
		}

		protected override void Dispose( bool disposing )
		{
			db.Dispose();
			base.Dispose( disposing );
		}
		public ActionResult Index()
		{
			ViewBag.Refresh = new string[] { "Addresses" };
			return View(this.Account);
		}
		[ChildActionOnly]
		public PartialViewResult Sidebar()
		{
			return PartialView("_Sidebar", this.Account);
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
		
	/*	#region CustomerInformation
			#region PaymentMethods
				[HttpGet]
				public ActionResult CreatePaymentMethod()
				{
					return View( "_CreatePaymentMethod" );
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult CreatePaymentMethod( PaymentMethodInput model )
				{
					if ( ModelState.IsValid )
					{
						PaymentMethod PaymentMethod = new PaymentMethod( );
						Account.PaymentMethods.Add( PaymentMethod );
						db.SaveChanges();
						return PartialView( "PaymentMethods", Account.PaymentMethods );
					}
					else
					{
						ModelState.AddModelError( "CreatePaymentMethodFailure", "There were errors with the Payment Method." );
						Response.StatusCode = 400;
						Response.StatusDescription = "<h4>There were errors:</h4><p>One or more of the fields could not be saved. Please review the form and ensure everything is valid.</p>";
						return PartialView( "_PaymentMethods", Account.PaymentMethods );
					}
				}
				[HttpGet]
				public PartialViewResult EditPaymentMethod( Guid id )
				{
					PaymentMethod PaymentMethod = db.PaymentMethods.Find( id );
					return PartialView( "_EditPaymentMethod", new PaymentMethodInput());
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult EditPaymentMethod( PaymentMethodInput model )
				{
					if ( ModelState.IsValid )
					{
						PaymentMethod PaymentMethod = new PaymentMethod();

						db.Entry( PaymentMethod ).State = EntityState.Modified;
						db.SaveChanges();
						return PartialView( "_PaymentMethods", Account.PaymentMethods );
					}
					return PartialView();
				}
				[HttpGet]
				public PartialViewResult DeletePaymentMethod( Guid id )
				{
					return PartialView( "_DeletePaymentMethod", db.PaymentMethods.Find( id ) );
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult DeletePaymentMethodConfirmed( Guid id )
				{
					try
					{
						if ( db.PaymentMethods.Any( c => c.RID == id ) )
						{
							db.PaymentMethods.Remove( db.PaymentMethods.Find( id ) );
							db.SaveChanges();
							return PartialView( "_PaymentMethods", Account.PaymentMethods );
						}
						else
						{
							Response.StatusCode = 400;
							Response.StatusDescription = "<h4>There were errors:</h4><p>There was a problem in deleting the Payment Method.</p>";
							return PartialView( "_PaymentMethods", Account.PaymentMethods );
						}
					}
					catch ( DbUpdateException e )
					{
						return PartialView( e );
					}

				}
				/*
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult CreatePaymentMethod(PaymentMethodInput model)
				{
					if ( ModelState.IsValid )
					{
						PaymentMethod card = new PaymentMethod();
						card.CardHolderName	= model.CardHolderName;
						card.CardNumber = model.CardNumber;
						card.CCV = model.CCV;
						card.Expiration = new DateTime(model.Year, model.Month, 1);
						card.BillingAddress = Account.Addresses.First(c => c.RID == model.BillingAddressID);
						/*
						if ( submitButton == "Create New Address" )
						{
							card.BillingAddress = new Address()
							{
								Line1 = PaymentMethod.BillingAddress.Line1,
								Line2 = PaymentMethod.BillingAddress.Line2,
								Nick = PaymentMethod.BillingAddress.Nick,
								ZIP = PaymentMethod.BillingAddress.ZIP,
								City = PaymentMethod.BillingAddress.CityTitle,
								State = db.States.Find(PaymentMethod.BillingAddress.StateID)
							};
							Account.Addresses.Add(card.BillingAddress);
							db.SaveChanges();
						}
				 
						else
						{
							card.BillingAddress = Account.Addresses.First( c => c.RID == PaymentMethod.BillingAddressID );
						}
						Account.PaymentMethods.Add( card );
						db.SaveChanges();
						return PartialView( "_PaymentMethods", Account.PaymentMethods );
					}
					return PartialView();
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult EditPaymentMethod(PaymentMethodInput model)
				{

					if ( ModelState.IsValid )
					{
						PaymentMethod card = new PaymentMethod();
						card.RID = model.RID;
						card.CardHolderName = model.CardHolderName;
						card.CardNumber = model.CardNumber;
						card.CCV = model.CCV;
						card.Expiration = new DateTime( model.Year, model.Month, 1 );
						card.BillingAddress = Account.Addresses.First( c => c.RID == model.BillingAddressID );
						db.Entry( card ).State = EntityState.Modified;
						db.SaveChanges();
					}
					return PartialView( "_PaymentMethods", Account.PaymentMethods);
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult DeletePaymentMethod(Guid id)
				{
					try
					{
						db.PaymentMethods.Remove( db.PaymentMethods.First( c => c.RID == id ) );
						db.SaveChanges();
						return PartialView( "_PaymentMethods", Account.PaymentMethods );
					}
					catch ( DbUpdateException e )
					{
						return PartialView( e );
					}
				}
			#endregion
			#region Orders
				[HttpGet]
				public PartialViewResult CreateOrder()
				{
					return PartialView( "_CreateOrder");
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult CreateOrder(Order Order)
				{
					return PartialView( "_Orders", Account.Orders );
				}
				[HttpGet]
				public PartialViewResult EditOrder(Guid id)
				{
					Order order = db.Orders.Find(id);
					return PartialView( "_EditOrder", new Order() );
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult EditOrder(Order Order)
				{
					return PartialView("_Orders", Account.Orders );
				}
				[HttpGet]
				public PartialViewResult CancelOrder( Guid id )
				{
					return PartialView( "_CancelOrder", db.Orders.Find( id ) );
				}
				[HttpPost]
				[ValidateAntiForgeryToken]
				public PartialViewResult CancelOrderConfirmed( Guid id )
				{
					try
					{
						if ( db.Orders.Any( c => c.RID == id ) )
						{
							var order = db.Orders.Find(id);
							order.Status = "Canceled";
							db.SaveChanges();
							return PartialView( "_Orders", Account.Orders );
						}
						else
						{
							Response.StatusCode = 400;
							Response.StatusDescription = "<h4>There were errors:</h4><p>We could not cancel your order.</p>";
							return PartialView( "_Orders", Account.Orders );
						}
					}
					catch ( DbUpdateException e )
					{
						return PartialView( e );
					}
				}
			#endregion
		#endregion */
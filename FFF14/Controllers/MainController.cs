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

namespace FFF.Controllers
{
	[RequireHttps]
	[Authorize]
	/// <summary>
	/// This Controller acts as a base for all User-Specific Controllers. 
	/// </summary>
    public class MainController : Controller
    {
		protected DatabaseContext db;
		protected Account Account;
		protected AuthenticationIdentityManager IdentityManager;
		protected Microsoft.Owin.Security.IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
     
	    public MainController() 
        {
			db = new DatabaseContext();
			IdentityManager = new AuthenticationIdentityManager(new IdentityStore(db));
        }
		protected override void Initialize( System.Web.Routing.RequestContext requestContext )
		{
			String name = requestContext.HttpContext.User.Identity.Name;
			if ( db.Accounts.Any( c => c.User.UserName == name ) )
			{
				this.Account = db.Accounts.First( c => c.User.UserName == name );
				if ( requestContext.HttpContext.Session["Account"] != null )
				{
					Account temp = requestContext.HttpContext.Session["Account"] as Account;
					requestContext.HttpContext.Session["Account"] = null;

					foreach ( var product in temp.ShoppingCart.Products )
					{
						product.Item = db.Items.Find(product.Item.RID);
						product.ShoppingCart = this.Account.ShoppingCart;
						db.SaveChanges();
						db.Products.Add(product);
						db.SaveChanges();
					}
				}
			}
			else if ( requestContext.HttpContext.Session["Account"] == null )
			{
				this.Account = new Account();
				this.Account.ShoppingCart = new ShoppingCart();
				requestContext.HttpContext.Session["Account"] = this.Account;
			}
			else
			{
				this.Account = requestContext.HttpContext.Session["Account"] as Account;
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
			return RedirectToAction("Index", "Store", new { Area="Store" } );
		}

	}
}
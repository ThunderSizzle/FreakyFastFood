using FFF.Hubs;
using FFF.Models.LocationSystem;
using FFF.ViewModels.Location;
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public override ActionResult Index()
		{
			return PartialView( "_Addresses", this.Account.Addresses );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult Create()
		{
			return PartialView( "_CreateAddress" );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public JsonResult CreateConfirmed( AddressInput model )
		{
			if ( ModelState.IsValid )
			{
				Address Address = new Address( model.Nick, model.Line1, model.Line2, model.City, db.States.Find( model.StateID ), model.ZIP );
				Account.Addresses.Add( Address );
				db.SaveChanges();
				//return PartialView( "_Addresses", Account.Addresses );
			}
			else
			{
				ModelState.AddModelError( "CreateAddressFailure", "There were errors with the Address." );
				Response.StatusCode = 400;
				Response.StatusDescription = "<h4>There were errors:</h4><p>One or more of the fields could not be saved. Please review the form and ensure everything is valid.</p>";
			}
			return Json( new { Value = "1", Text = "Address" });
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult Edit( Guid id )
		{
			Address Address = db.Addresses.Find( id );
			return PartialView( "_EditAddress", new AddressInput( Address.RID, Address.Nick, Address.Line1, Address.Line2, Address.City, Address.State, Address.ZIP ) );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public JsonResult EditConfirmed( AddressInput model )
		{
			if ( ModelState.IsValid )
			{
				Address Address = db.Addresses.Find( model.RID );
				Address.Nick = model.Nick;
				Address.Line1 = model.Line1;
				Address.Line2 = model.Line2;
				Address.City = model.City;
				Address.State = db.States.Find( model.StateID );
				Address.ZIP = model.ZIP;

				db.Entry( Address ).State = EntityState.Modified;
				db.SaveChanges();
			}
			else
			{
				ModelState.AddModelError( "CreateAddressFailure", "There were errors with the Address." );
				Response.StatusCode = 400;
				Response.StatusDescription = "<h4>There were errors:</h4><p>One or more of the fields could not be saved. Please review the form and ensure everything is valid.</p>";
			}
			return Json( new { Value = "1", Text = "Address" } );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult Delete( Guid id )
		{
			return PartialView( "_DeleteAddress", db.Addresses.Find( id ) );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public JsonResult DeleteConfirmed( Guid id )
		{
			if ( Account.Addresses.Any( c => c.RID == id ) )
			{
				Address Address = Account.Addresses.First( c => c.RID == id );
				if ( Address.Payments.Count == 0 && Address.ShoppingCarts.Count == 0 && Address.Orders.Count == 0 )
				{
					db.Addresses.Remove( Address );
				}
				Account.Addresses.Remove( Address );
				db.SaveChanges();
			}
			else
			{
				Response.StatusCode = 404;
				Response.StatusDescription += "<ul>We could not find the Address. Please refresh your page and try again.</ul>";
			}
			return Json( new { Value = "1", Text = "Address" } );
		}
	}
}
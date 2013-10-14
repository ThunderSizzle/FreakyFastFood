using FFF.Models.LocationSystem;
using FFF.Models.PaymentSystem;
using FFF.ViewModels.Location;
using FFF.ViewModels.PaymentMethod;
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public override ActionResult Index()
		{
			return PartialView( "_PaymentMethods", this.Account.PaymentMethods );
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public ActionResult Create( PaymentMethodInput model = null)
		{
			ViewBag.CardTypes = new SelectList(db.CardTypes.ToList(), "RID", "Title");
			ViewBag.Months = new SelectList(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
			var years = new Collection<int>();
			for(int i = 0; i <= 20; i++)
			{
				years.Add( i + DateTime.Now.Year );
			}
			ViewBag.Years = new SelectList( years );
			return PartialView( "_CreatePaymentMethod", model );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult CreateCallback( PaymentMethodInput model )
		{
			if ( ModelState.IsValid )
			{
				ViewBag.Addresses = this.Account.Addresses.ToList();
				if( this.Account.Addresses.Count > 0)
				{
					return PartialView( "_BillingAddressSelector", model as SelectedAddressInput );
				}
				else
				{
					return PartialView( "_NewBillingAddress", model as NewAddressInput );
				}
			}
			return PartialView();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult SelectAddress( NewAddressInput model )
		{
			if ( ModelState.IsValid )
			{
				ViewBag.Addresses = this.Account.Addresses.ToList();
				if ( this.Account.Addresses.Count > 0 )
				{
					return PartialView( "_BillingAddressSelector", model as PaymentMethodInput as SelectedAddressInput );
				}
				else
				{
					return PartialView( "_NewBillingAddress", model );
				}
			}
			return PartialView();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult NewAddress( SelectedAddressInput model )
		{
			if ( ModelState.IsValid )
			{
				return PartialView( "_BillingAddressSelector", model as PaymentMethodInput as NewAddressInput );
			}
			return PartialView();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult CreateWithNewAddress( NewAddressInput model )
		{
			if ( ModelState.IsValid )
			{
				Address Address = new Address( model.Address.Nick, model.Address.Line1, model.Address.Line2, model.Address.City, db.States.Find( model.Address.StateID ), model.Address.ZIP );
				PaymentMethod PaymentMethod = new PaymentMethod
				(
					model.CardHolderName,
					db.CardTypes.Find(model.CardTypeID),
					model.CardNumber,
					new DateTime( model.Year, model.Month, 1 ),
					model.CCV,
					Address
				);
				Account.PaymentMethods.Add( PaymentMethod );
				try
				{ 
					db.SaveChanges();
					Account.Addresses.Add( Address );
					db.SaveChanges();
				}
				catch (Exception ex)
				{

				}
				return PartialView( "_PaymentMethods", Account.PaymentMethods );
			}
			return PartialView();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult CreateWithSelectedAddress( SelectedAddressInput model )
		{
			if ( ModelState.IsValid )
			{
				PaymentMethod PaymentMethod = new PaymentMethod
				(
					model.CardHolderName,
					db.CardTypes.Find( model.CardTypeID ),
					model.CardNumber,
					new DateTime( model.Year, model.Month, 1 ),
					model.CCV,
					db.Addresses.Find(model.AddressID)
				);
				Account.PaymentMethods.Add( PaymentMethod );
				db.SaveChanges();
				return PartialView( "_PaymentMethods", Account.PaymentMethods );
			}
			return PartialView();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult Edit( Guid id )
		{
			PaymentMethod PaymentMethod = db.PaymentMethods.Find( id );
			return PartialView( "_EditPaymentMethod", new PaymentMethodInput() );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult Edit( PaymentMethodInput model )
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
		public PartialViewResult DeletePaymentMethod( Guid id )
		{
			return PartialView( "_DeletePaymentMethod", db.PaymentMethods.Find( id ) );
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AjaxOnly]
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
		}*/
	}
}
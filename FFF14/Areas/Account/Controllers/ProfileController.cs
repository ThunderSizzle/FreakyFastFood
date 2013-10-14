using FFF.Models.ContactSystem;
using FFF.ViewModels.Account;
using FFF.ViewModels.Profile;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FFF.Areas.Account.Controllers
{
	[RequireHttps]
	[ValidateAntiForgeryToken]
	public class ProfileController : FFF.Controllers.MainController
    {
		[HttpPost]
		[ValidateAntiForgeryToken]
		public override ActionResult Index()
		{
			return PartialView( "_Profile", this.Account );
		}
		#region Personal Profile
			[HttpPost]
			[AjaxOnly]
			public PartialViewResult EditPersonal()
			{
				PersonalInput Personal = new PersonalInput( this.Account.RID, this.Account.FirstName, this.Account.LastName, this.Account.Gender, this.Account.User.UserName );

				ViewBag.Gender = new SelectList( db.Genders.ToList(), "RID", "Title", Account.Gender );

				return PartialView( "_EditPersonal", Personal );
			}
			[HttpPost]
			[AjaxOnly]
			public PartialViewResult EditPersonalConfirmed( PersonalInput model )
			{
				if ( ModelState.IsValid )
				{
					this.Account.User.UserName = model.UserName;
					this.Account.FirstName = model.FirstName;
					this.Account.LastName = model.LastName;
					this.Account.Gender = db.Genders.First( c => c.RID ==  model.GenderID );
					db.Entry( Account ).State = EntityState.Modified;
					db.SaveChanges();
					FormsAuthentication.SetAuthCookie(this.Account.User.UserName, true);
					return PartialView( "_Personal", Account );
				}
				else
				{
					ModelState.AddModelError( "CreateAddressFailure", "There were errors with the Address." );
					Response.StatusCode = 400;
					Response.StatusDescription = "<h4>There were errors:</h4><p>One or more of the fields could not be saved. Please review the form and ensure everything is valid.</p>";
					return PartialView( "_Personal", Account );
				}
			}
		#endregion
		#region Contact Profile
			#region Phones
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult CreatePhone()
				{
					ViewBag.Carriers = new SelectList(db.Carriers.ToList(), "RID", "Title");
					return PartialView("_CreatePhone");
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult CreatePhoneConfirmed( PhoneInput model )
				{
					//todo Finish Method
					if(ModelState.IsValid)
					{
						Phone Phone = new Phone(db.Carriers.Find(model.CarrierID), model.AreaCode, model.Prefix, model.Line);
						Account.Phones.Add(Phone);
						if(model.defaultMethod)
							Account.DefaultPhone = Phone;
						db.SaveChanges();
					}
					else
					{

					}
					return PartialView( "_Contact", Account );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult EditPhone( Guid id )
				{
					Phone Phone = db.Phones.Find(id);
					PhoneInput model = new PhoneInput(Phone.RID, Phone.Carrier.RID, Phone.AreaCode, Phone.Prefix, Phone.Line, Phone.OrderUpdates, this.Account.DefaultPhone == Phone);
					ViewBag.Carriers = new SelectList( db.Carriers.ToList(), "RID", "Title", Phone.Carrier);
					return PartialView( "_EditPhone", model );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult EditPhoneConfirmed( PhoneInput model )
				{
					//todo Finish Method
					if ( ModelState.IsValid )
					{
						
					}
					else
					{

					}
					return PartialView( "_Contact", Account );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult DeletePhone( Guid id )
				{
					//todo Finish Method
					return PartialView( "_DeletePhone", db.Phones.Find( id ) );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult DeletePhoneConfirmed( Guid id )
				{
					//todo Finish Method
					return PartialView( "_Contact", Account );
				}
			#endregion
			#region Email
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult CreateEmail()
				{
					//todo Finish Method
					return PartialView( "_CreateEmail" );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult CreateEmailConfirmed( EmailInput model )
				{
					//todo Finish Method
					return PartialView( "_Contact", Account );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult EditEmail( Guid id )
				{
					Email Email = db.Emails.Find( id );
					EmailInput model = new EmailInput( Email.RID, Email.EmailAddress, Email.OrderUpdates, Email.SpecialOffers, Email == Account.DefaultEmail );
					return PartialView( "_EditEmail", model );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult EditEmailConfirmed( EmailInput model )
				{
					//todo Finish Method
					return PartialView( "_Contact", Account );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult DeleteEmail( Guid id )
				{
					//todo Finish Method
					return PartialView( "_DeleteEmail" );
				}
				[HttpPost]
				[AjaxOnly]
				public PartialViewResult DeleteEmailConfirmed( Guid id )
				{
					//todo Finish Method
					return PartialView( "_Contact", Account );
				}
			#endregion
		#endregion
		#region FoodProfile
		//todo This will be done after we establish our Item System More.
		#endregion
	}
}
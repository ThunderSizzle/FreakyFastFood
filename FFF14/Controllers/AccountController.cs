using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using FFF.Models;
using FFF.Models.UserSystem;
using System.Threading;
using System.Web.Security;
using FFF.Models.OrderSystem;
using System.Data.Entity;
using FFF.ViewModels.Location;
using FFF.Models.LocationSystem;
using System.Data.Entity.Infrastructure;
using FFF.ViewModels.PaymentMethod;
using FFF.Models.PaymentSystem;
using FFF.Models.ContactSystem;
using FFF.ViewModels.Account;
using FFF.ViewModels.Profile;

namespace FFF.Controllers
{
	/// <summary>
	/// This Controller handles all Account Actions, such as Logging In, Registering, Changing Credentials, etc.
	/// </summary>
    [Authorize]
	[RequireHttps]
    public class AccountController : MainController
    {
		#region Account System
			#region Logging System
				public ActionResult Index()
				{
					return View(this.Account);
				}
				public PartialViewResult Sidebar()
				{
					return PartialView("_Sidebar", this.Account);
				}
				[AllowAnonymous]
				public ActionResult Login( String returnUrl )
				{
					ViewBag.ReturnUrl = returnUrl;
					return View("Login");
				}
				//
				// POST: /Account/Disassociate
				[HttpPost]
				[ValidateAntiForgeryToken]
				public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
				{
					ManageMessageId? message = null;
					string userId = Account.User.Id;

					var result = await IdentityManager.Logins.RemoveLoginAsync( Account.User.Id, loginProvider, providerKey );
					if ( result.Success )
					{
						message = ManageMessageId.RemoveLoginSuccess;
					}

					return RedirectToAction("Manage", new { Message = message });
				}

				//
				// GET: /Account/Manage
				public async Task<ActionResult> Manage(ManageMessageId? message)
				{
					ViewBag.StatusMessage =
						message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
						: message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
						: message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
						: "";
					ViewBag.HasLocalPassword = await IdentityManager.Logins.HasLocalLoginAsync(Account.User.Id);
					ViewBag.ReturnUrl = Url.Action("Manage");
					return View();
				}

				//
				// POST: /Account/Manage
				[HttpPost]
				[ValidateAntiForgeryToken]
				public async Task<ActionResult> Manage(ManageUserViewModel model)
				{
					string userId = Account.User.Id;
					bool hasLocalLogin = await IdentityManager.Logins.HasLocalLoginAsync(userId);
					ViewBag.HasLocalPassword = hasLocalLogin;
					ViewBag.ReturnUrl = Url.Action("Manage");
					if (hasLocalLogin)
					{               
						if (ModelState.IsValid)
						{
							var result = await IdentityManager.Passwords.ChangePasswordAsync(User.Identity.GetUserName(), model.OldPassword, model.NewPassword);
							if (result.Success)
							{
								return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
							}
							else
							{
								ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
							}
						}
					}
					else
					{
						// User does not have a local password so remove any validation errors caused by a missing OldPassword field
						ModelState state = ModelState["OldPassword"];
						if (state != null)
						{
							state.Errors.Clear();
						}

						if (ModelState.IsValid)
						{
							try
							{
								// Create the local login info and link the local account to the user
								var result = await IdentityManager.Logins.AddLocalLoginAsync( userId, User.Identity.GetUserName(), model.NewPassword );
								if ( result.Success )
								{
									return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
								}
								else
								{
									ModelState.AddModelError("", "Failed to set password");
								}
							}
							catch (Exception e)
							{
								ModelState.AddModelError("", e);
							}
						}
					}

					// If we got this far, something failed, redisplay form
					return View(model);
				}

				//
				// POST: /Account/ExternalLogin
				[HttpPost]
				[AllowAnonymous]
				[ValidateAntiForgeryToken]
				public ActionResult ExternalLogin(string provider, string returnUrl)
				{
					// Request a redirect to the external login provider
					return new ChallengeResult( provider, Url.Action( "ExternalLoginCallback", "Account", new { loginProvider = provider, ReturnUrl = returnUrl } ) );
				}

				//
				// GET: /Account/ExternalLoginCallback
				[AllowAnonymous]
				public async Task<ActionResult> ExternalLoginCallback(string loginProvider, string returnUrl)
				{
					ClaimsIdentity id = await IdentityManager.Authentication.GetExternalIdentityAsync(AuthenticationManager);
					var signin = await IdentityManager.Authentication.SignInExternalIdentityAsync( AuthenticationManager, id );
			//		var email = id.Claims.First( c => c.Type == ClaimTypes.Email ).Value;

					if ( signin.Success )
					{
					//	this.Account = db.Accounts.First( c => c.User.UserName == id.N );
						return RedirectToLocal( returnUrl );
					}
					else if (User.Identity.IsAuthenticated)
					{
						// Try to link if the user is already signed in
						var result = await IdentityManager.Authentication.LinkExternalIdentityAsync( id, Account.User.Id );
						if ( result.Success )
						{
						//	this.Account.Emails.Add(new Email(email));
							return RedirectToLocal(returnUrl);
						}
						else 
						{
							return View("ExternalLoginFailure");
						}
					}
					else
					{
						// Otherwise prompt to create a local user
						ViewBag.ReturnUrl = returnUrl;
						ViewBag.Gender = new SelectList( db.Genders.ToList(), "RID", "Title", Account.Gender );
						return PartialView( "ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = id.Name, Email = null, LoginProvider = loginProvider } );
					}
				}
				//
				// POST: /Account/ExternalLoginConfirmation
				[HttpPost]
				[AllowAnonymous]
				[ValidateAntiForgeryToken]
				public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
				{
					if (User.Identity.IsAuthenticated)
					{
						return RedirectToAction("Manage");
					}
            
					if (ModelState.IsValid)
					{
						FFFUser user = new FFFUser( model.UserName );
						// Get the information about the user from the external login provider
						var result = await IdentityManager.Authentication.CreateAndSignInExternalUserAsync( AuthenticationManager, user , CancellationToken.None );
						if ( result.Success )
						{
							db.SaveChanges();
							this.Account = user.Account;
							db.SaveChanges();
							this.Account.User = user;
							db.SaveChanges();
							this.Account.ShoppingCart = new ShoppingCart();
							db.SaveChanges();
							Email Email = new FFF.Models.ContactSystem.Email(model.Email);
							this.Account.Emails.Add( Email );
							this.Account.DefaultEmail = Email;
							this.Account.FirstName = model.FirstName;
							this.Account.LastName = model.LastName;
							this.Account.Gender = db.Genders.First(c => c.RID ==  model.GenderID);
							db.SaveChanges();
							FormsAuthentication.SetAuthCookie( model.UserName, false );
							if(returnUrl == Url.Action("Index", "Store"))
								return RedirectToAction("Index", "Account");
							else
								return RedirectToLocal(returnUrl);
						}
						else
						{
							return View("ExternalLoginFailure");
						}
					}

					ViewBag.ReturnUrl = returnUrl;
					return View(model);
				}

				//
				// POST: /Account/LogOff
				[HttpPost]
				[ValidateAntiForgeryToken]
				public ActionResult LogOff()
				{
					FormsAuthentication.SignOut();
					AuthenticationManager.SignOut();
					this.Account = null;
					return RedirectToAction("Index", "Store");
				}

				//
				// GET: /Account/ExternalLoginFailure
				[AllowAnonymous]
				public ActionResult ExternalLoginFailure()
				{
					return View();
				}

				[AllowAnonymous]
				[ChildActionOnly]
				public ActionResult ExternalLoginsList(string returnUrl)
				{
					ViewBag.ReturnUrl = returnUrl;
					return (ActionResult)PartialView("_ExternalLoginsListPartial", new List<AuthenticationDescription>(AuthenticationManager.GetExternalAuthenticationTypes()));
				}

				[ChildActionOnly]
				public ActionResult RemoveAccountList()
				{
					return Task.Run(async () =>
					{
						var linkedAccounts = await IdentityManager.Logins.GetLoginsAsync(Account.User.Id);
						ViewBag.ShowRemoveButton = linkedAccounts.Count() > 1;
						return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
					}).Result;
				}

				protected override void Dispose(bool disposing) {
					if (disposing && IdentityManager != null) {
						IdentityManager.Dispose();
						IdentityManager = null;
					}
					base.Dispose(disposing);
				}
			#endregion
			#region Helpers
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

				private class ChallengeResult : HttpUnauthorizedResult
				{
					public ChallengeResult(string provider, string redirectUrl)
					{
						LoginProvider = provider;
						RedirectUrl = redirectUrl;
					}

					public string LoginProvider { get; set; }
					public string RedirectUrl { get; set; }

					public override void ExecuteResult(ControllerContext context)
					{
						context.HttpContext.GetOwinContext().Authentication.Challenge( new AuthenticationProperties() { RedirectUrl = RedirectUrl }, LoginProvider);
					}
				}
        
				public enum ManageMessageId
				{
					ChangePasswordSuccess,
					SetPasswordSuccess,
					RemoveLoginSuccess,
				}
        
			#endregion
		#endregion
		
		
		#region CustomerInformation
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
				}*/
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
		#endregion
    }
}
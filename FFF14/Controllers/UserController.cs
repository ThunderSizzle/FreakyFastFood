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
    public class UserController : MainController
    {
		#region Logging System
			public ActionResult Index()
			{
				return RedirectToAction("Index", "Account", new { area="Account" } );
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
				return new ChallengeResult( provider, Url.Action( "ExternalLoginCallback", "User", new { loginProvider = provider, ReturnUrl = returnUrl } ) );
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
						//FormsAuthentication.SetAuthCookie( model.UserName, false );
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
    }
}
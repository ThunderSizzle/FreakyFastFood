using FFF.Models;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Controllers;

namespace System.Web.Mvc
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AjaxOnlyAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!filterContext.HttpContext.Request.IsAjaxRequest())
			{
				filterContext.HttpContext.Response.StatusCode = 404;
				filterContext.Result = new HttpNotFoundResult();
			}
			else
			{
				base.OnActionExecuting(filterContext);
			}
		}
	}
}
namespace System.ComponentModel.DataAnnotations
{
	public class CardTypeVerifyAttribute : ValidationAttribute
	{
		public override bool IsValid( object value )
		{
			if ( value.GetType() == typeof( PaymentMethod ) )
			{
				PaymentMethod method = value as PaymentMethod;
				if ( Regex.IsMatch( method.CardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$" ) )
				{
					return method.CardType.Title == "Visa";
				}
				else if ( Regex.IsMatch( method.CardNumber, @"^5[1-5][0-9]{14}$" ) )
				{
					return method.CardType.Title == "MasterCard";
				}
				else if ( Regex.IsMatch( method.CardNumber, @"^3[47][0-9]{13}$" ) )
				{
					return method.CardType.Title == "American Express";
				}
				else if ( Regex.IsMatch( method.CardNumber, @"^6(?:011|5[0-9]{2})[0-9]{12}$" ) )
				{
					return method.CardType.Title == "Discover";
				}
				return false;
			}
			return false;
		}
	}
}

namespace System.Web.Http.Filters
{
	[SuppressMessage( "Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Unsealed because type contains virtual extensibility points." )]
	[AttributeUsage( AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false )]
    
	public class RequireHttpsAttribute : AuthorizationFilterAttribute
	{
		public override void OnAuthorization( HttpActionContext actionContext )
		{
			if ( actionContext == null )
			{
				throw new ArgumentNullException( "actionContext" );
			}

			if ( actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps )
			{
				HandleNonHttpsRequest( actionContext );
			}
			else
			{
				base.OnAuthorization( actionContext );
			}
		}

		protected virtual void HandleNonHttpsRequest( HttpActionContext actionContext )
		{
			actionContext.Response = new HttpResponseMessage( System.Net.HttpStatusCode.Forbidden );
			actionContext.Response.ReasonPhrase = "SSL Required";
		}
	}
}
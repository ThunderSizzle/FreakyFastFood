using System.Web.Mvc;

namespace FFF.Areas.Account
{
    public class AccountAreaAreaRegistration : AreaRegistration 
	{
        public override string AreaName 
		{
            get 
			{
                return "Account";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
		{
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { controller="Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
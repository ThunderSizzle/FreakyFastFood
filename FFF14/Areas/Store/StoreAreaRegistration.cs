using System.Web.Mvc;

namespace FFF.Areas.Store
{
    public class StoreAreaAreaRegistration : AreaRegistration 
	{
        public override string AreaName 
		{
            get 
			{
                return "Store";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
		{
            context.MapRoute(
                "Default",
                "Store/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
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
                "Store_default",
                "Store/{controller}/{action}/{id}",
                new { controller = "Store", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
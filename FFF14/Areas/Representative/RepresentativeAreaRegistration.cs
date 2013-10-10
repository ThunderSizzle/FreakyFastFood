using System.Web.Mvc;

namespace FFF.Areas.Representative
{
    public class RepresentativeAreaAreaRegistration : AreaRegistration 
	{
        public override string AreaName 
		{
            get 
			{
                return "Representative";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
		{
            context.MapRoute(
                "Representative_default",
                "Representative/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
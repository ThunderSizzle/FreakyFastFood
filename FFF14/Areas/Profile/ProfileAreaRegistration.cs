using System.Web.Mvc;

namespace FFF.Areas.Profile
{
    public class ProfileAreaAreaRegistration : AreaRegistration 
	{
        public override string AreaName 
		{
            get 
			{
                return "Profile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
		{
            context.MapRoute(
                "Profile_default",
                "Profile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
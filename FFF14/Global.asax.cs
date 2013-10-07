using EnhancedViewLocations;
using FFF.Models;
using FFF.Models.LocationSystem;
using FFF.ViewModels.Location;
using Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FFF
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			DatabaseContext db = new DatabaseContext();
			Database.SetInitializer<DatabaseContext>( new CreateDatabaseIfNotExists<DatabaseContext>() );
			db.Database.Initialize( true );

			FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
			RouteConfig.RegisterRoutes( RouteTable.Routes );
			BundleConfig.RegisterBundles( BundleTable.Bundles );
			RegisterCustomViewLocations();
		}

		private static void RegisterCustomViewLocations()
		{
			// We can optionally enable the standard Razor view CS (or VB) locations 
			// so we don't always search our extra / custom areas first when
			// searching for a view.
			EnhancedViewLocator.EnableStandardRazorCSLocations();

			EnhancedViewLocator.AddFolder( "views/account/employee" );
			EnhancedViewLocator.AddFolder( "views/account/representative" );

			EnhancedViewLocator.AddFolder( "views/shared/address" );
			EnhancedViewLocator.AddFolder( "views/shared/paymentmethod" );
			EnhancedViewLocator.AddFolder( "views/shared/order" );
			EnhancedViewLocator.AddFolder( "views/shared/review" );
			EnhancedViewLocator.AddFolder( "views/shared/shoppingcart" );
			EnhancedViewLocator.AddFolder( "views/shared/layouts" );
			EnhancedViewLocator.AddFolder( "views/shared/profile" );
			EnhancedViewLocator.AddFolder( "views/shared/profile/contact" );
			EnhancedViewLocator.AddFolder( "views/shared/profile/contact/email" );
			EnhancedViewLocator.AddFolder( "views/shared/profile/contact/phone" );
			EnhancedViewLocator.AddFolder( "views/shared/profile/personal" );
			EnhancedViewLocator.AddFolder( "views/shared/profile/food" );
			EnhancedViewLocator.AddFolder( "views/shared/settings" );
			EnhancedViewLocator.AddFolder( "views/shared/item" );
			EnhancedViewLocator.AddFolder( "views/shared/product" );

			// Finally, have the locator install a custom view engine to manage the lookups.
			EnhancedViewLocator.Install( ControllerBuilder.Current );
		}
	}
}

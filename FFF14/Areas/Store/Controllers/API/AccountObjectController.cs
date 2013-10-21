using FFF.Models;
using FFF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace FFF.Areas.Store.Controllers.API
{
	[Authorize]
    public abstract class StoreObjectController : ApiController
    {
		protected DatabaseContext db = new DatabaseContext();
		protected FFF.Models.Account Account;

		protected override void Initialize( System.Web.Http.Controllers.HttpControllerContext controllerContext )
		{
			var name = controllerContext.RequestContext.Principal.Identity.Name;
			this.Account = db.Accounts.First( c => c.User.UserName == name );
			base.Initialize( controllerContext );
		}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
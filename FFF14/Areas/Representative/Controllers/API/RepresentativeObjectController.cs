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

namespace FFF.Areas.Representative.Controllers.API
{
	[Authorize(Roles="Representative")]
    public abstract class RepresentativeObjectController : ApiController
    {
		protected DatabaseContext db = new DatabaseContext();
		protected FFF.Models.Representative Representative;

		protected override void Initialize( System.Web.Http.Controllers.HttpControllerContext controllerContext )
		{
			var name = controllerContext.RequestContext.Principal.Identity.Name;
			this.Representative = db.Accounts.First( c => c.User.UserName == name ) as FFF.Models.Representative;
			base.Initialize( controllerContext );
		}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
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

namespace FFF.Areas.Employee.Controllers.API
{
	[Authorize(Roles="Employee")]
    public abstract class EmployeeObjectController : ApiController
    {
		protected DatabaseContext db = new DatabaseContext();
		protected FFF.Models.Employee Employee;

		protected override void Initialize( System.Web.Http.Controllers.HttpControllerContext controllerContext )
		{
			var name = controllerContext.RequestContext.Principal.Identity.Name;
			this.Employee = db.Accounts.First( c => c.User.UserName == name ) as FFF.Models.Employee;
			base.Initialize( controllerContext );
		}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
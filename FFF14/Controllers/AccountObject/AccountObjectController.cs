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
using System.Web.Http.Filters;

namespace FFF.Controllers.AccountObject
{
	[Authorize]
	[RequireHttps]
    public abstract class AccountObjectController : ApiController
    {
		protected DatabaseContext db = new DatabaseContext();
		protected FFF.Models.Account Account;

		protected AccountObjectController()
			: base()
		{

		}
		protected AccountObjectController( FFF.Models.Account Account )
			: base()
		{
			this.Account = db.Accounts.First(c => c.RID == Account.RID);
		}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
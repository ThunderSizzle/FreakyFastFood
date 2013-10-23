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

namespace FFF.Areas.Account.Controllers.API
{
	[Authorize]
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
			this.Account = Account;
		}
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
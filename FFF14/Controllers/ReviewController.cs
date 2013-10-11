using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFF.Models.ReviewSystem;
using FFF.Models;
using FFF.Models.ProfileSystem;
using System.Data.Entity.Infrastructure;

namespace FFF.Controllers
{
	/// <summary>
	/// This controller allows a User to see all of his or her own reviews, and allows the creation of new reviews.
	/// </summary>
	[RequireHttps]
	[Authorize]
    public class ReviewController : MainController
    {
        //
        // GET: /Review/
        public ActionResult Index()
        {
			return PartialView( Account.Reviews.ToList() );
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ReportReview( Guid id )
		{
			db.Reviews.Find( id ).Reported = true;
			db.SaveChanges();
			return PartialView();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ReportHelpful ( Guid id )
		{
			db.Reviews.Find( id ).AddHelpful();
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult ReportUnHelpful ( Guid id )
		{
			db.Reviews.Find( id ).AddUnHelpful();
			return View();
		}


        //
        // GET: /Review/Details/5
        public ActionResult Details(Guid id)
        {
			if ( Account.Reviews.Any( c => c.RID == id ) )
			{
				return View( Account.Reviews.First( c => c.RID == id ) );
			}
			return HttpNotFound();
        }

		//
		// POST: /Payment/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete ( Guid id )
		{
			try
			{
				if ( db.Reviews.Any( c => c.RID == id ) )
				{
					db.Reviews.Remove( db.Reviews.First( c => c.RID == id ) );
					db.SaveChanges();
					return Json( new { success = "true" } );
				}
				return HttpNotFound();
			}
			catch ( DbUpdateException e )
			{
				return Json( new { success = "false" } );
			}
		}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

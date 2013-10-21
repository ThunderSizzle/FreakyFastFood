using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;

namespace FFF.Areas.Account.Controllers
{
	/// <summary>
	/// This controller allows a User to see all of his or her own reviews, and allows the creation of new reviews.
	/// </summary>
	[RequireHttps]
	[Authorize]
	public class ReviewController : FFF.Controllers.MainController
	{
		[HttpGet]
		public PartialViewResult Index()
		{
			return PartialView( "_Reviews" );
		}
		[HttpGet]
		public PartialViewResult Details()
		{
			return PartialView( "_DetailsReview" );
		}
		[HttpGet]
		public PartialViewResult Create()
		{
			return PartialView( "_CreateReview" );
		}
		[HttpGet]
		public PartialViewResult Edit()
		{
			return PartialView( "_EditReview" );
		}
		[HttpGet]
		public PartialViewResult Delete()
		{
			return PartialView( "_DeleteReview" );
		}
    }
}

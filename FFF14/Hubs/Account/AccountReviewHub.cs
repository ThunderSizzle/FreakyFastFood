using FFF.Areas.Account.Controllers.API;
using FFF.InputModels;
using FFF.Models;
using FFF.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace FFF.Hubs
{
	[Authorize]
	public class AccountReviewHub : AccountObjectHub
	{
		public override async Task Index()
		{
			using ( ReviewController api = new ReviewController( await this.Account() ) )
			{
				var Reviews = api.Get();
				ICollection<ReviewView> ReviewsView = new Collection<ReviewView>();
				foreach ( var address in Reviews )
				{
					ReviewsView.Add( new ReviewView() );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).IndexBack( ReviewsView );
			}
		}
		public async Task Post( ReviewInput model )
		{
			using ( ReviewController api = new ReviewController( await this.Account() ) )
			{
				var result = await api.Post( model );
				var contentresult = result as OkNegotiatedContentResult<Review>;
				if ( contentresult != null )
				{
					ReviewView ReviewView = new ReviewView(
						//contentresult.Content.
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PostBack( ReviewView );
				}
				else
				{
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
				}
			}
		}
		public async Task Put( ReviewInput model )
		{
			using ( ReviewController api = new ReviewController( await this.Account() ) )
			{
				var result = await api.Put( model );
				var contentresult = result as OkNegotiatedContentResult<Address>;
				if ( contentresult != null )
				{
					ReviewView ReviewView = new ReviewView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PutBack( ReviewView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
		public override async Task Delete( Guid ReviewRID )
		{
			using ( ReviewController api = new ReviewController( await this.Account() ) )
			{
				var result = await api.Delete( ReviewRID );
				var contentresult = result as OkNegotiatedContentResult<Review>;
				if ( contentresult != null )
				{
					ReviewView ReviewView = new ReviewView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).DeleteBack( ReviewView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
	}
}
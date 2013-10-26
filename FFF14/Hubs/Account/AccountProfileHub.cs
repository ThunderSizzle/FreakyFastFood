using FFF.Controllers.AccountObject;
using FFF.InputModels;
using FFF.Models;
using FFF.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace FFF.Hubs
{
	[Authorize]
	public class AccountProfileHub : AccountObjectHub
	{
		public override async Task Index()
		{
			using ( ProfileController api = new ProfileController( await this.Account() ) )
			{
				var Profiles = api.Get();
				ProfileView ProfileView = new ProfileView();

				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).IndexBack( ProfileView );
			}
		}
		public async Task Post( ProfileInput model )
		{
			using ( ProfileController api = new ProfileController( await this.Account() ) )
			{
				var result = await api.Post( model );
				var contentresult = result as OkNegotiatedContentResult<Profile>;
				if ( contentresult != null )
				{
					ProfileView ProfileView = new ProfileView(
						//contentresult.Content.
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PostBack( ProfileView );
				}
				else
				{
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
				}
			}
		}
		public async Task Put( ProfileInput model )
		{
			using ( ProfileController api = new ProfileController( await this.Account() ) )
			{
				var result = await api.Put( model );
				var contentresult = result as OkNegotiatedContentResult<Address>;
				if ( contentresult != null )
				{
					ProfileView ProfileView = new ProfileView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PutBack( ProfileView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
		public override async Task Delete( Guid ProfileRID )
		{
			using ( ProfileController api = new ProfileController( await this.Account() ) )
			{
				var result = await api.Delete( ProfileRID );
				var contentresult = result as OkNegotiatedContentResult<Profile>;
				if ( contentresult != null )
				{
					ProfileView ProfileView = new ProfileView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).DeleteBack( ProfileView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
	}
}
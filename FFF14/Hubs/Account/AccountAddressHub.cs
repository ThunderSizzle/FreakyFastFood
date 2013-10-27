using FFF.Controllers.AccountObject;
using FFF.DropDownModels;
using FFF.InputModels;
using FFF.Models;
using FFF.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace FFF.Hubs
{
	[Authorize]
	
	public class AccountAddressHub : AccountObjectHub
	{
		public override async Task Index()
		{
			using( AddressController api = new AddressController(await this.Account()) )
			{
				var Addresses = api.Get();
				ICollection<AddressView> AddressesView = new Collection<AddressView>();
				foreach ( var address in Addresses )
				{
					AddressesView.Add( new AddressView( address ) );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).indexBack( AddressesView );
			}
		}
		public async Task Post( AddressInput model)
		{
			using ( AddressController api = new AddressController( await this.Account() ) )
			{
				var result = await api.Post(model);
				var contentresult = result as OkNegotiatedContentResult<Address>;
				if(contentresult != null)
				{
					AddressView Address = new AddressView( 
						contentresult.Content.RID, 
						contentresult.Content.Nick,
						contentresult.Content.Line1,
						contentresult.Content.Line2,
						contentresult.Content.City,
						contentresult.Content.State.Abbreviation,
						contentresult.Content.State.RID,
						contentresult.Content.ZIP
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).postBack( Address );
				}
				else
				{ 
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).error( result );
				}
			}
		}
		public async Task Put( AddressInput model )
		{
			using ( AddressController api = new AddressController( await this.Account() ) )
			{
				var result = await api.Put( model );
				var contentresult = result as OkNegotiatedContentResult<Address>;
				if ( contentresult != null )
				{
					AddressView Address = new AddressView(
						contentresult.Content.RID,
						contentresult.Content.Nick,
						contentresult.Content.Line1,
						contentresult.Content.Line2,
						contentresult.Content.City,
						contentresult.Content.State.Abbreviation,
						contentresult.Content.State.RID,
						contentresult.Content.ZIP
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).putBack( Address );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).error( result );
			}
		}
		public override async Task Delete( Guid AddressRID )
		{
			using ( AddressController api = new AddressController( await this.Account() ) )
			{
				var result = await api.Delete( AddressRID );
				var contentresult = result as OkNegotiatedContentResult<Address>;
				if ( contentresult != null )
				{
					AddressView Address = new AddressView(
						contentresult.Content.RID,
						contentresult.Content.Nick,
						contentresult.Content.Line1,
						contentresult.Content.Line2,
						contentresult.Content.City,
						contentresult.Content.State.Abbreviation,
						contentresult.Content.State.RID,
						contentresult.Content.ZIP
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).deleteBack( Address );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).error( result );
			}
		}
		public async Task StateList( AddressInput model )
		{
			var States = await db.States.ToListAsync();
			ICollection<StateDropDownItem> StateList = new Collection<StateDropDownItem>();
			foreach ( var state in States )
			{
				StateList.Add( new StateDropDownItem( state.RID, state.Title ) );
			}
			Clients.Clients( ( await this.ConnectionIds() ).ToList() ).stateListBack( StateList );
		}
	}
}
using FFF.Areas.Account.Controllers.API;
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
			using( AddressController api = new AddressController() )
			{
				var Addresses = api.Get();
				ICollection<AddressView> AddressesView = new Collection<AddressView>();
				foreach ( var address in Addresses )
				{
					AddressesView.Add( new AddressView( address.RID, address.Nick, address.Line1, address.Line2, address.City, address.State.Abbreviation, address.ZIP ) );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).IndexBack( AddressesView );
			}
		}
		public async Task Post( AddressInput model)
		{
			using ( AddressController api = new AddressController() )
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
						contentresult.Content.ZIP
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PostBack( Address );
				}
				else
				{ 
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
				}
			}
		}
		public async Task Put( AddressInput model )
		{
			using ( AddressController api = new AddressController() )
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
						contentresult.Content.ZIP
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PutBack( Address );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
		public override async Task Delete( Guid AddressRID )
		{
			using ( AddressController api = new AddressController() )
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
						contentresult.Content.ZIP
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).DeleteBack( Address );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
		public async Task StateList()
		{
			var States = await db.States.ToListAsync();
			ICollection<StateDropDownItem> StateList = new Collection<StateDropDownItem>();
			foreach ( var state in States )
			{
				StateList.Add( new StateDropDownItem( state.RID, state.Title ) );
			}
			Clients.Clients( ( await this.ConnectionIds() ).ToList() ).StateListBack( StateList );
		}
	}
}
using FFF.Controllers.AccountObject;
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
	public class AccountShoppingCartHub : AccountObjectHub
	{
		public override async Task Index()
		{
			using ( ShoppingCartController api = new ShoppingCartController( await this.Account() ) )
			{
				var ShoppingCart = api.Get();
				var ShoppingCartView = new ShoppingCartView(ShoppingCart);
				Clients.Caller.IndexBack( ShoppingCartView );
			}
		}
		public async Task Post( SettingInput model )
		{
			using ( SettingController api = new SettingController( await this.Account() ) )
			{
				var result = await api.Post( model );
				var contentresult = result as OkNegotiatedContentResult<Setting>;
				if ( contentresult != null )
				{
					SettingView SettingView = new SettingView(
						//contentresult.Content.
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PostBack( SettingView );
				}
				else
				{
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
				}
			}
		}
		public async Task Put( SettingInput model )
		{
			using ( SettingController api = new SettingController( await this.Account() ) )
			{
				var result = await api.Put( model );
				var contentresult = result as OkNegotiatedContentResult<Setting>;
				if ( contentresult != null )
				{
					SettingView SettingView = new SettingView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PutBack( SettingView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
		public override async Task Delete( Guid SettingRID )
		{
			using ( SettingController api = new SettingController( await this.Account() ) )
			{
				var result = await api.Delete( SettingRID );
				var contentresult = result as OkNegotiatedContentResult<Address>;
				if ( contentresult != null )
				{
				//	SettingView SettingView = new SettingView(
						//contentresult.Content.RID,
				//	);
				//	Clients.Clients( ( await this.ConnectionIds() ).ToList() ).DeleteBack( SettingView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
	}
}
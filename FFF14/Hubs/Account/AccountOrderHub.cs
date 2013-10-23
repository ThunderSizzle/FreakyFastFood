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
	public class AccountOrderHub : AccountObjectHub
	{
		public override async Task Index()
		{
			using ( OrderController api = new OrderController( await this.Account() ) )
			{
				var Orders = api.Get();
				ICollection<OrderView> OrdersView = new Collection<OrderView>();
				foreach ( var order in Orders )
				{
					OrdersView.Add( new OrderView( ) );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).IndexBack( OrdersView );
			}
		}
		public async Task Post( OrderInput model )
		{
			using ( OrderController api = new OrderController( await this.Account() ) )
			{
				var result = await api.Post( model );
				var contentresult = result as OkNegotiatedContentResult<Order>;
				if ( contentresult != null )
				{
					OrderView OrderView = new OrderView(
						//contentresult.Content.
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PostBack( OrderView );
				}
				else
				{
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
				}
			}
		}
		public async Task Put( OrderInput model )
		{
			using ( OrderController api = new OrderController( await this.Account() ) )
			{
				var result = await api.Put( model );
				var contentresult = result as OkNegotiatedContentResult<Order>;
				if ( contentresult != null )
				{
					OrderView OrderView = new OrderView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PutBack( OrderView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
		public override async Task Delete( Guid OrderRID )
		{
			using ( OrderController api = new OrderController( await this.Account() ) )
			{
				var result = await api.Delete( OrderRID );
				var contentresult = result as OkNegotiatedContentResult<Order>;
				if ( contentresult != null )
				{
					OrderView OrderView = new OrderView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).DeleteBack( OrderView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
	}
}
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
	[Authorize]
	public class AccountPaymentMethodHub : AccountObjectHub
	{
		public override async Task Index()
		{
			using ( PaymentMethodController api = new PaymentMethodController( await this.Account() ) )
			{
				var PaymentMethods = api.Get();
				ICollection<PaymentMethodView> PaymentMethodsView = new Collection<PaymentMethodView>();
				foreach ( var address in PaymentMethods )
				{
					PaymentMethodsView.Add( new PaymentMethodView() );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).IndexBack( PaymentMethodsView );
			}
		}
		public async Task Post( PaymentMethodInput model)
		{
			using ( PaymentMethodController api = new PaymentMethodController( await this.Account() ) )
			{
				var result = await api.Post(model);
				var contentresult = result as OkNegotiatedContentResult<PaymentMethod>;
				if(contentresult != null)
				{
					PaymentMethodView PaymentMethodView = new PaymentMethodView( 
						//contentresult.Content.
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PostBack( PaymentMethodView );
				}
				else
				{ 
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
				}
			}
		}
		public async Task Put( PaymentMethodInput model )
		{
			using ( PaymentMethodController api = new PaymentMethodController( await this.Account() ) )
			{
				var result = await api.Put( model );
				var contentresult = result as OkNegotiatedContentResult<PaymentMethod>;
				if ( contentresult != null )
				{
					PaymentMethodView PaymentMethodView = new PaymentMethodView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).PutBack( PaymentMethodView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
		public override async Task Delete( Guid PaymentMethodRID )
		{
			using ( PaymentMethodController api = new PaymentMethodController( await this.Account() ) )
			{
				var result = await api.Delete( PaymentMethodRID );
				var contentresult = result as OkNegotiatedContentResult<Address>;
				if ( contentresult != null )
				{
					PaymentMethodView PaymentMethodView = new PaymentMethodView(
						//contentresult.Content.RID,
					);
					Clients.Clients( ( await this.ConnectionIds() ).ToList() ).DeleteBack( PaymentMethodView );
				}
				Clients.Clients( ( await this.ConnectionIds() ).ToList() ).Error( result );
			}
		}
	}
}
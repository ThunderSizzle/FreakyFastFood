using FFF.Models;
using FFF.ViewModels;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace FFF.Hubs
{
	[AllowAnonymous]
	public abstract class StoreObjectHub : DatabaseObjectHub
	{
		public abstract Task Index();
		public abstract Task Delete(Guid RID);
		protected async Task<ICollection<String>> ConnectionIds()
		{
			var Account = await db.Accounts.FirstAsync( c => c.User.UserName == Context.User.Identity.Name );
			ICollection<String> ConnectionIds = new Collection<String>();
			foreach ( var item in ( Account.User as FFFUser ).Connections )
			{
				ConnectionIds.Add( item.ConnectionID );
			}
			return ConnectionIds;
		}
		public override Task OnConnected()
		{
			var name = Context.User.Identity.Name;

			var User = db.Users.First( c => c.UserName == name ) as FFFUser;
			if ( User != null )
			{
				var connection = User.Connections.FirstOrDefault( c => c.ConnectionID == Context.ConnectionId );
				if ( connection == null )
				{
					User.Connections.Add( new Connection
					{
						ConnectionID = Context.ConnectionId,
						UserAgent = Context.Request.Headers["User-Agent"],
						Connected = true
					} );
					db.SaveChanges();
				}
			}
			else
			{
				Clients.Caller.showErrorMessage( "We could not find your user account." );
			}
			db.SaveChanges();

			return base.OnConnected();
		}
		public override Task OnDisconnected()
		{
			var connection = db.Connections.FirstOrDefault( c => c.ConnectionID == Context.ConnectionId );
			connection.Connected = false;
			db.SaveChanges();
			return base.OnDisconnected();
		}
	}
}
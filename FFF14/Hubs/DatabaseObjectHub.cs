using FFF.Models;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FFF.Hubs
{
	public abstract class DatabaseObjectHub : Hub
	{
		protected DatabaseContext db = new DatabaseContext();
		public override Task OnConnected()
		{
			FFFUser User = null;
			if ( Context.User != null )
			{
				var name = Context.User.Identity.Name;
				User = db.Users.FirstOrDefault( c => c.UserName == name ) as FFFUser;
			}
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
				}
			}
			else
			{
				var Account = new Account();
				Account.ShoppingCart = new ShoppingCart();
				Account.User = new FFFUser( Guid.NewGuid().ToString() );
				( Account.User as FFFUser ).Connections.Add( new Connection
				{
					ConnectionID = Context.ConnectionId,
					UserAgent = Context.Request.Headers["User-Agent"],
					Connected = true
				} );
				db.Accounts.Add(Account);
			}
			db.SaveChanges();
			return base.OnConnected();
		}
		public override Task OnDisconnected()
		{
			var connection = db.Connections.FirstOrDefault( c => c.ConnectionID == Context.ConnectionId );
			if(Context.User != null)
			{
				var name = Context.User.Identity.Name;
				var User = db.Users.FirstOrDefault( c => c.UserName == name ) as FFFUser;

			}
			else if( connection.User.UserName == null )
			{
				foreach(var product in connection.User.Account.ShoppingCart.Products)
				{
					db.Products.Remove( product );
				}
				db.ShoppingCarts.Remove( connection.User.Account.ShoppingCart );
				db.Accounts.Remove( connection.User.Account );
				db.Users.Remove( connection.User );
				db.SaveChanges();
			}
			connection.Connected = false;
			db.SaveChanges();
			return base.OnDisconnected();
		}
		protected override void Dispose( bool disposing )
		{
			db.Dispose();
			base.Dispose( disposing );
		}
	}
}
using FFF.Models;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace FFF.Hubs
{
	[Authorize]
	public abstract class DatabaseObjectHub : Hub
	{
		protected DatabaseContext db = new DatabaseContext();
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
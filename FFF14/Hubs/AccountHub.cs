using FFF.Models;
using FFF.Models.UserSystem;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFF.Hubs
{

	[Authorize]
	public class AccountHub : Hub
	{
		DatabaseContext db = new DatabaseContext();

		public void AddressList()
		{
			
			Connection Connection = db.Connections.First( c => c.ConnectionID == Context.ConnectionId);
			
			Clients.Client(Connection.ConnectionID).updateAddressList("This is the server message. If we smart, we can update the div with this.");
		}

		public override System.Threading.Tasks.Task OnConnected()
		{
			var name = Context.User.Identity.Name;

			FFFUser user = db.Users.FirstOrDefault(c => c.UserName == name);
			if(user != null)
			{
				user.Connections.Add(new Connection
                {
                    ConnectionID = Context.ConnectionId,
                    UserAgent = Context.Request.Headers["User-Agent"],
                    Connected = true
                });
			}
			else
			{
				Clients.Caller.showErrorMessage("We could not find your user account.");
			}
			db.SaveChanges();

			return base.OnConnected();
		}

		public override System.Threading.Tasks.Task OnDisconnected()
		{
            var connection = db.Connections.Find(Context.ConnectionId);
            connection.Connected = false;
            db.SaveChanges();
			return base.OnDisconnected();
		}
	}
}
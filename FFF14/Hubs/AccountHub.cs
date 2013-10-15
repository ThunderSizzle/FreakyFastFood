using FFF.Models;
using FFF.Models.UserSystem;
using FFF.ViewModels.Location;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FFF.Hubs
{
	[Authorize]
	public class AccountHub : Hub
	{
		DatabaseContext db = new DatabaseContext();
		FFFUser User;
		public void GetAll()
		{
			var Account = db.Accounts.First(c => c.User.UserName == Context.User.Identity.Name);
			var Addresses = Account.Addresses;
			if(Addresses.Count  == 0)
			{
				Clients.Client(Context.ConnectionId).addressesAll();			
			}
			else
			{
				ICollection<AddressView> AddressesView = new Collection<AddressView>();
				foreach(var address in Addresses)
				{
					AddressesView.Add(new AddressView(address.RID, address.Nick, address.Line1, address.Line2, address.City, address.State.Abbreviation, address.ZIP));
				}
				ICollection<String> ConnectionIds = new Collection<String>();
				foreach (var item in (Account.User as FFFUser).Connections)
				{
					ConnectionIds.Add(item.ConnectionID);
				}

				Clients.Clients(ConnectionIds.ToList()).addressesAll(AddressesView);
			}
		}
		public override System.Threading.Tasks.Task OnConnected()
		{
			var name = Context.User.Identity.Name;

			this.User = db.Users.First( c => c.UserName == name ) as FFFUser;
			if (this.User != null)
			{
				var connection = this.User.Connections.FirstOrDefault(c => c.ConnectionID == Context.ConnectionId);
				if(connection == null)
				{
					this.User.Connections.Add(new Connection
					{
						ConnectionID = Context.ConnectionId,
						UserAgent = Context.Request.Headers["User-Agent"],
						Connected = true
					});
				}
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
			this.User = null;
			var connection = db.Connections.FirstOrDefault(c => c.ConnectionID == Context.ConnectionId);
            connection.Connected = false;
            db.SaveChanges();
			return base.OnDisconnected();
		}
	}
}
using FFF.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FFF.Hubs
{
	[Authorize(Roles="Representative")]
	public abstract class RepresentativeObjectHub : DatabaseObjectHub
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
	}
}
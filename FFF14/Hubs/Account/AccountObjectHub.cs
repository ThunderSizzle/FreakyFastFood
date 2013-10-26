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
	public abstract class AccountObjectHub : DatabaseObjectHub
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

		protected async Task<Account> Account()
		{
			var name = Context.User.Identity.Name;
			Account Account;
			Account = await db.Accounts.FirstAsync( c => c.User.UserName == name);
			return Account;
		}
	}
}
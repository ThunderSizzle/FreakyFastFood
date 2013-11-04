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
	/// <summary>
	/// This class provides an Interface for an Account Object being modified by the Account Owner.
	/// All child classes only allow modification to the object from the Account Owner, and will broadcast the change to only the account owner and any other privledged user (ex: admin, manager).
	/// </summary>
	public abstract class AccountObjectHub : DatabaseObjectHub
	{
		public abstract Task Index();
		public abstract Task Delete(Guid RID);
		protected async Task<ICollection<String>> ConnectionIds()
		{
			var Account = await this.Account();
			ICollection<String> ConnectionIds = new Collection<String>();
			foreach ( var item in ( Account.User as FFFUser ).Connections )
			{
				ConnectionIds.Add( item.ConnectionID );
			}
			//todo: Add Admins and Managers to the ConnecionIds List.
			return ConnectionIds;
		}
		protected async Task<Account> Account()
		{			
			var name = Context.User.Identity.Name;
			if(name != null)
			{
				return await db.Accounts.FirstAsync( c => c.User.UserName == name );
			}
			else
			{
				return (await db.Connections.FindAsync(Context.ConnectionId)).User.Account;
			}
		}
	}
}
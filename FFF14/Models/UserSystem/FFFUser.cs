using FFF.ViewModels.Account;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FFF.Models.UserSystem
{
	public class FFFUser : User
	{
		//todo Comment Class
		public virtual Account Account { get; set; }
		public virtual ICollection<Connection> Connections { get; set; }

		public FFFUser()
			: base()
		{
			this.Connections = new Collection<Connection>();
		}
		public FFFUser( RegisterViewModel Register )
			: this()
		{
			this.UserName = Register.UserName;
		}
		public FFFUser ( String UserName )
			: this()
		{
			this.UserName = UserName;
		}
	}
}
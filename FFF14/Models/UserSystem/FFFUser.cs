using FFF.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FFF.Models
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
		public FFFUser ( String UserName )
			: base( UserName )
		{
			this.Connections = new Collection<Connection>();
		}
	}
}
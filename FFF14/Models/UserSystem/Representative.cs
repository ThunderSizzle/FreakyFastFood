using FFF.Models;
using FFF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FFF.Models
{
	//todo Comment Class
	public class Representative : Account
	{
		public virtual ICollection<Chain> Chains { get; set; }
		public virtual ICollection<Restaurant> Restaurants { get; set; }

		public Representative( )
			: base( )
		{
			this.Chains = new Collection<Chain>();
			this.Restaurants = new Collection<Restaurant>();
		}
	}
}
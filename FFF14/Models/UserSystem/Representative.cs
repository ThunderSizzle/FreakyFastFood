using FFF.Models.FoodProviderSystem;
using FFF.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FFF.Models.UserSystem
{
	//todo Comment Class
	public class Representative : Account
	{
		public virtual ICollection<Chain> Chains { get; set; }
		public virtual ICollection<Restaurant> Restaurants { get; set; }

		public Representative ( RegisterViewModel Register )
			: base (Register)
		{
			this.Chains = new Collection<Chain>();
			this.Restaurants = new Collection<Restaurant>();
		}
		public Representative( )
			: base( )
		{
			this.Chains = new Collection<Chain>();
			this.Restaurants = new Collection<Restaurant>();
		}
	}
}
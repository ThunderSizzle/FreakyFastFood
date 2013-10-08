using FFF.Models.ContactSystem;
using FFF.Models.LocationSystem;
using FFF.Models.ProfileSystem;
using FFF.Models.UserSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models.FoodProviderSystem
{
	//todo Comment Class
	public class Restaurant : Provider
	{
		public Chain Chain { get; set; }
		public Address Address { get; set; }
		public ICollection<Representative> Representatives { get; set; }

		public Restaurant()
			: base ()
		{
			this.Representatives = new Collection<Representative>();
			this.Address = new Address();
			this.Chain = new Chain();
		}
		public Restaurant(Chain Chain)
			: base()
		{
			this.Chain = Chain;
			this.Address = new Address();
			this.Representatives = new Collection<Representative>();
		}
		public Restaurant( Chain Chain, Address Address )
			: base()
		{
			this.Chain = Chain;
			this.Address = Address;
			this.Representatives = new Collection<Representative>();
		}
	}
}
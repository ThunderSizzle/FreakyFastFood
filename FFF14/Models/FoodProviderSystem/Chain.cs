using FFF.Models.ContactSystem;
using FFF.Models.ItemSystem;
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
	public class Chain : Provider
	{
		public String Title { get; set; }
		public String Description { get; set; }
		public virtual ICollection<Representative> Representatives { get; set; }
		public virtual ICollection<Menu> Menus { get; set; }
		public virtual ICollection<Restaurant> Restaurants { get; set; }
		public Chain()
			: base()
		{
			this.Menus = new Collection<Menu>();
			this.Restaurants = new Collection<Restaurant>();
			this.Representatives = new Collection<Representative>();
		}
		public Chain( String Title )
			: base()
		{
			this.Title = Title;
			this.Menus = new Collection<Menu>();
			this.Restaurants = new Collection<Restaurant>();
			this.Representatives = new Collection<Representative>();
		}
	}
}
using FFF.Models.FoodProviderSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFF.Models.ItemSystem
{
	//todo Comment Class
	public class Menu : DatabaseObject
	{
		public String Title { get; set; }
		public virtual Chain Chain { get; set; }
		public virtual ICollection<Item> Items { get; set; }

		public Menu()
			: base()
		{
			this.Items = new Collection<Item>();
		}
		public Menu(String Title, Chain Chain)
			: base()
		{
			this.Title = Title;
			this.Chain = Chain;
			this.Items = new Collection<Item>();
		}
	}
}
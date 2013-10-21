using FFF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Comment Class
	public class Chain : Provider
	{
		public String Title { get; set; }
		public String Description { get; set; }
		public virtual ICollection<Representative> Representatives { get; set; }
		public virtual ICollection<Item> Items { get; set; }
		public virtual ICollection<Restaurant> Restaurants { get; set; }
		public override bool Removeable
		{
			get
			{
				if ( Representatives.Count + Items.Count + Restaurants.Count > 0 )
					return false;
				else
					return base.Removeable;
			}
		}
		public Chain()
			: base()
		{
			this.Items = new Collection<Item>();
			this.Restaurants = new Collection<Restaurant>();
			this.Representatives = new Collection<Representative>();
		}
		public Chain( String Title )
			: base()
		{
			this.Title = Title;
			this.Items = new Collection<Item>();
			this.Restaurants = new Collection<Restaurant>();
			this.Representatives = new Collection<Representative>();
		}
	}
}
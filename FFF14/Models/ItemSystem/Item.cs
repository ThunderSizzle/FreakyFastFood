using FFF.Models.ImagesSystem;
using FFF.Models.OrderSystem;
using FFF.Models.ReviewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models.ItemSystem
{
	//todo Comment Class
	public abstract class Item : ImageOwner
	{
		public virtual Menu Menu { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public Decimal Price { get; set; }
		public virtual ICollection<Category> Categories { get; set; }
		/// <summary>
		/// A List of Choices for a Item. 
		/// </summary>
		public virtual ICollection<Choice> Choices { get; set; }
		public virtual ICollection<Product> Products { get; set; }

		public Item()
			: base()
		{
			this.Choices = new Collection<Choice>();
			this.Categories = new Collection<Category>();
			this.Products = new Collection<Product>();
		}
		public Item(Menu Menu, String Title, String Description, Decimal Price = 0M)
			: this()
		{
			this.Menu = Menu;
			this.Title = Title;
			this.Description = Description;
			this.Price = Price;
		}
	}
}
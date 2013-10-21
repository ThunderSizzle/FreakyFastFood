using FFF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Comment Class
	public abstract class Item : ImageOwner
	{
		[Required]
		public virtual Chain Chain { get; set; }
		[Required]
		public String Title { get; set; }
		public String Description { get; set; }
		public Decimal Price { get; set; }
		public virtual ICollection<Category> Categories { get; set; }
		/// <summary>
		/// A List of Choices for a Item. 
		/// </summary>
		public virtual ICollection<Choice> Choices { get; set; }
		public virtual ICollection<Product> Products { get; set; }

		public override bool Removeable
		{
			get
			{
				if(Categories.Count + Choices.Count + Products.Count > 0)
					return false;
				else
					return base.Removeable;
			}
		}

		public Item()
			: base()
		{
			this.Choices = new Collection<Choice>();
			this.Categories = new Collection<Category>();
			this.Products = new Collection<Product>();
		}
		public Item( Chain Chain, String Title, String Description, Decimal Price = 0M )
			: this()
		{
			this.Chain = Chain;
			this.Title = Title;
			this.Description = Description;
			this.Price = Price;
		}
	}
}
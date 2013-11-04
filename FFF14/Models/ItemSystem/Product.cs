using FFF.InputModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace FFF.Models
{
	public class Product : ItemDatabaseObject
	{
		public decimal Price
		{
			get
			{
				Decimal price = this.Item.Price;
				foreach(var selectedOption in this.SelectedOptions)
				{
					price += selectedOption.AdditionalPrice;
				}
				return price;
			}
		}
		[Required]
		public virtual Item Item { get; set; }
		[Required]
		public virtual ICollection<Option> SelectedOptions { get; set; }
		public virtual ShoppingCart ShoppingCart { get; set; }
		public virtual Order Order { get; set; }
		public override bool Removeable
		{
			get
			{
				if ( Order != null || ShoppingCart != null )
					return false;
				else
					return base.Removeable;
			}
		}
		public Product()
			: base()
		{
			this.SelectedOptions = new Collection<Option>();
		}
		public Product(Item Item, params Option[] SelectedOptions )
			: base()
		{
			this.Item = Item;
			this.SelectedOptions = new Collection<Option>();
			if(SelectedOptions != null)
			{ 
				foreach(var selectedOption in SelectedOptions)
				{
					this.SelectedOptions.Add( selectedOption );
				}
			}
		}
		public Product(Item Item, ProductInput model)
			: base()
		{
			this.Item = Item;
			this.SelectedOptions = model.SelectedOptions;
		}

	}
}

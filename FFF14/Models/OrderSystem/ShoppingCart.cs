using FFF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Comment Class

	public class ShoppingCart : Reviewable
	{
		public virtual Account Account { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual Address DeliveryAddress { get; set; }
		public virtual Decimal Subtotal
		{
			get
			{
				Decimal subtotal = 0;
				foreach ( var product in this.Products )
				{
					subtotal += product.Price;
				}
				return subtotal;
			}
		}
		public override bool Removeable
		{
			get
			{
				return false;
			}
		}
		public ShoppingCart()
			: base()
		{
			this.Products = new Collection<Product>();
		}
	}
}
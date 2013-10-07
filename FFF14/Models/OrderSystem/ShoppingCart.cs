using FFF.Models.ItemSystem;
using FFF.Models.LocationSystem;
using FFF.Models.PaymentSystem;
using FFF.Models.ReviewSystem;
using FFF.Models.UserSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFF.Models.OrderSystem
{
	//todo Comment Class

	public class ShoppingCart : Reviewable
	{
		public virtual Account Account { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual Address DeliveryAddress { get; set; }
		public virtual PaymentMethod PaymentMethod { get; set; }
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
		public ShoppingCart()
			: base()
		{
			this.Products = new Collection<Product>();
		}
	}
}
using FFF.Models.UserSystem;
using FFF.Models.ItemSystem;
using FFF.Models.ReviewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FFF.Models.PaymentSystem;
using FFF.Models.LocationSystem;

namespace FFF.Models.OrderSystem
{
	//todo Comment Class
	
	public class Order : Reviewable
	{
		public virtual Account Account { get; set; }
		public DateTime CreateStamp { get; set; }
		public virtual ICollection<Product> Products { get; set; }
		public virtual PaymentMethod PaymentMethod { get; set; }
		public virtual Address DeliveryAddress { get; set; }
		public string Status { get; set; }
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

		public Order()
			: base()
		{
			this.Products = new Collection<Product>();
		}
	}
}
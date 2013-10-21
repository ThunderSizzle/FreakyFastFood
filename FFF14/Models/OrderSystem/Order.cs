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
		public override bool Removeable
		{
			get
			{
				return false;
			}
		}
		public Order()
			: base()
		{
			this.Products = new Collection<Product>();
		}
	}
}
using FFF.Models.OrderSystem;
using FFF.Models.PaymentSystem;
using FFF.Models.ProfileSystem;
using FFF.Models.UserSystem;
using FFF.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace FFF.Models.LocationSystem
{
	//todo Comment Class
	public class Address : DatabaseObject
	{

		public virtual Profile Profile { get; set; }
		[DataType( DataType.Text )]
		[Display( Name = "NickName" )]
		public String Nick { get; set; }
		[Required]
		[DataType( DataType.Text )]
		[Display( Name = "Address Line 1", Description = "Street Address, Company Name" )]
		public String Line1 { get; set; }
		[DataType( DataType.Text )]
		[Display( Name = "Address Line 2", Description = "Apartment, suite, unit, building, floor, etc." )]
		public String Line2 { get; set; }
		[Required]
		[Display( Name = "City" )]
		public String City { get; set; }
		[Required]
		[Display( Name = "State" )]
		public virtual State State { get; set; }
		[Required]
		[DataType( DataType.PostalCode )]
		[Display( Name = "ZIP Code" )]
		[StringLength( 5 )]
		public String ZIP { get; set; }
		public virtual ICollection<PaymentMethod> Payments { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
		public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

		public Address()
			: base()
		{
			this.Payments = new Collection<PaymentMethod>();
			this.Orders = new Collection<Order>();
			this.ShoppingCarts = new Collection<ShoppingCart>();
		}
		public Address ( String Nick, String Line1, String Line2, String City, State State, String ZIP )
			: this()
		{
			this.Nick = Nick;
			this.Line1 = Line1;
			this.Line2 = Line2;
			this.City = City;
			this.State = State;
			this.ZIP = ZIP;
		}
		public Address( Guid ID, String Nick, String Line1, String Line2, String City, State State, String ZIP, Profile Profile )
			: this()
		{
			this.RID = ID;
			this.Nick = Nick;
			this.Line1 = Line1;
			this.Line2 = Line2;
			this.City = City;
			this.State = State;
			this.ZIP = ZIP;
			this.Profile = Profile;
		}
	}
}
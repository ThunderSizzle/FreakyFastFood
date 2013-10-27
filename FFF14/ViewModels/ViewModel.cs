using FFF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFF.ViewModels
{
	public abstract class ViewModel
	{
		public Guid RID { get; set; }

		public ViewModel()
		{

		}
		public ViewModel( Guid RID )
		{
			this.RID = RID;
		}
	}
	public class AddressView : ViewModel
	{
		[Display( Name="Nick Name", Description="A friendly name to help distinguish this Address for yourself and our deliverers" )]
		public String Nick { get; set; }
		[Required]
		[Display( Name="Line 1" )]
		public String Line1 { get; set; }
		[Display( Name="Line 2" )]
		public String Line2 { get; set; }
		[Required]
		[Display( Name="City" )]
		public String City { get; set; }
		[Display( Name="State", Description="State Abbreviation" )]
		[Required]
		public String State { get; set; }
		[Display( Name="State ID", Description="State ID" )]
		[Required]
		public Guid StateRID { get; set; }
		[Display( Name="ZIP Code" )]
		[Required]
		public String ZIP { get; set; }

		public AddressView()
			: base()
		{

		}
		public AddressView(Address Address)
			: base(Address.RID)
		{
			this.Nick = Address.Nick;
			this.Line1 = Address.Line1;
			this.Line2 = Address.Line2;
			this.City = Address.City;
			this.State = Address.State.Abbreviation;
			this.StateRID = Address.State.RID;
			this.ZIP = Address.ZIP;
		}
		public AddressView( Guid RID, string Nick, string Line1, string Line2, string City, string State, Guid StateRID, String ZIP )
			: base( RID )
		{
			this.Nick = Nick;
			this.Line1 = Line1;
			this.Line2 = Line2;
			this.City = City;
			this.State = State;
			this.StateRID = StateRID;
			this.ZIP = ZIP;
		}
	}
	public class OrderView : ViewModel
	{
		public DateTime CreateStamp { get; set; }
		public ICollection<Product> Products { get; set; }
		public PaymentMethodView PaymentMethod { get; set; }
		public AddressView DeliveryAddress { get; set; }
		public string Status { get; set; }
		public Decimal Subtotal
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

		public OrderView()
			: base()
		{
			Products = new Collection<Product>();
		}
		public OrderView(Order Order)
			: base(Order.RID)
		{
			this.CreateStamp = Order.CreateStamp;
			this.Products = Order.Products;
			this.PaymentMethod = new PaymentMethodView(Order.PaymentMethod);
			this.DeliveryAddress = new AddressView(Order.DeliveryAddress);
			this.Status = Order.Status;
		}
	}
	public class PaymentMethodView : ViewModel
	{
		[Required]
		[Display( Name = "Credit Card Holder's Name" )]
		public String CardHolderName { get; set; }
		[Required]
		[Display( Name = "Credit Card Type" )]
		public Guid CardTypeID { get; set; }
		[Required]
		[DataType( DataType.CreditCard )]
		[Display( Name = "Credit Card Number" )]
		[MinLength( 15 )]
		[MaxLength( 19 )]
		[CreditCard]
		public String CardNumber { get; set; }
		[Required]
		[Display( Name = "Month" )]
		public int Month { get; set; }
		[Required]
		[Display( Name = "Year" )]
		public int Year { get; set; }
		[Required]
		[DataType( DataType.Text )]
		[Display( Name = "Security Code (CCV)" )]
		[MinLength( 3 )]
		[MaxLength( 4 )]
		public String CCV { get; set; }

		public PaymentMethodView()
			: base()
		{

		}
		public PaymentMethodView(Guid RID, String CardHolderName, Guid CardTypeID, String CardNumber, int Month, int Year, String CCV)
			: base (RID)
		{
			this.CardHolderName = CardHolderName;
			this.CardTypeID = CardTypeID;
			this.CardNumber = CardNumber;
			this.Month = Month;
			this.Year = Year;
			this.CCV = CCV;
		}
		public PaymentMethodView( PaymentMethod PaymentMethod )
			: base( PaymentMethod.RID )
		{
			this.CardHolderName = PaymentMethod.CardHolderName;
			this.CardTypeID = PaymentMethod.CardType.RID;
			this.CardNumber = PaymentMethod.CardNumber;
			this.Month = PaymentMethod.Expiration.Month;
			this.Year = PaymentMethod.Expiration.Year;
			this.CCV = PaymentMethod.CCV;
		}
	}
	public class ReviewView : ViewModel
	{
		/// <summary>
		/// Poster's RID, used to link to the user's public profile page.
		/// </summary>
		public Guid PosterID { get; set; }
		public String PosterUserName { get; set; }
		public String Message { get; set; }
		public DateTime Timestamp { get; set; }
		public int Helpful { get; set; }
		public int Unhelpful { get; set; }
		public int Rating { get; set; }

		public ReviewView()
			: base()
		{

		}
		public ReviewView( Review Review )
			: base(Review.RID)
		{
			this.PosterID = Review.Poster.RID;
			this.PosterUserName = Review.Poster.User.UserName;
			this.Message = Review.Message;
			this.Timestamp = Review.Timestamp;
			this.Helpful = Review.Helpful;
			this.Unhelpful = Review.Unhelpful;
			this.Rating = Review.Rating;
		}
		public ReviewView(Guid RID, Account Account, String Message, DateTime Timestamp, int helpful, int unhelpful, int rating)
			: base(RID)
		{
			this.PosterID = Account.RID;
			this.PosterUserName = Account.User.UserName;
			this.Message = Message;
			this.Timestamp = Timestamp;
			this.Helpful = Helpful;
			this.Unhelpful = Unhelpful;
			this.Rating = Rating;
		}
	}
	public class ProfileView : ViewModel
	{
		public String UserName { get; set; }
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public DateTime Birthday { get; set; }
		public String Gender { get; set; }
		public ProfileView()
			: base()
		{

		}
		public ProfileView(Account Account)
			: base(Account.RID)
		{
			this.Birthday = Account.Birthday;
			this.FirstName = Account.FirstName;
			this.LastName = Account.LastName;
			this.Gender = Account.Gender.Title;
			this.UserName = Account.User.UserName;
		}
		public ProfileView( Guid RID, DateTime Birthday, String FirstName, String LastName, Gender Gender, String UserName )
			: base( RID )
		{
			this.Birthday = Birthday;
			this.FirstName = FirstName;
			this.LastName = LastName;
			this.Gender = Gender.Title;
			this.UserName = UserName;
		}
	}
	public class SettingView : ViewModel
	{
		public SettingView()
			: base()
		{

		}
	}
	public class ExternalLoginConfirmationViewModel : ViewModel
	{
		[Required]
		[Display( Name = "User Name" )]
		public string UserName { get; set; }
		[Required]
		[Display( Name = "Email Address" )]
		public string Email { get; set; }
		[Required]
		public String FirstName { get; set; }
		public String LastName { get; set; }
		public Guid GenderID { get; set; }
		[Required]
		public string LoginProvider { get; set; }
	}
}
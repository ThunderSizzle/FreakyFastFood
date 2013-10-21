using System;
using System.Collections.Generic;
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
		[Display( Name="ZIP Code" )]
		[Required]
		public String ZIP { get; set; }

		public AddressView()
			: base()
		{

		}

		public AddressView( Guid RID, string Nick, string Line1, string Line2, string City, string State, String ZIP )
			: base( RID )
		{
			this.Nick = Nick;
			this.Line1 = Line1;
			this.Line2 = Line2;
			this.City = City;
			this.State = State;
			this.ZIP = ZIP;
		}
	}
	public class OrderView : ViewModel
	{

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
	}
	public class ReviewView : ViewModel
	{
		public FFF.Models.Account Poster { get; set; }
		public String Message { get; set; }
		public DateTime Timestamp { get; set; }
		public int Helpful { get; set; }
		public int Unhelpful { get; set; }
		public int Rating { get; set; }
	}
	public class ProfileView : ViewModel
	{

	}
	public class SettingView : ViewModel
	{

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
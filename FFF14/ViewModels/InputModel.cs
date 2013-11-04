using FFF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFF.InputModels
{
	public abstract class InputModel
	{
		public Guid RID { get; set; }

		public InputModel()
		{

		}
		public InputModel(Guid RID)
		{
			this.RID = RID;
		}
	}

	public class AddressInput : InputModel
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
		[Display( Name="State" )]
		[Required]
		public Guid StateID { get; set; }
		[Display( Name="ZIP Code" )]
		[Required]
		public String ZIP { get; set; }

		public AddressInput()
			: base()
		{

		}

		public AddressInput( Guid RID, string Nick, string Line1, string Line2, string City, State State, String ZIP )
			: base( RID )
		{
			this.Nick = Nick;
			this.Line1 = Line1;
			this.Line2 = Line2;
			this.City = City;
			this.StateID = State.RID;
			this.ZIP = ZIP;
		}
	}
	public class OrderInput : InputModel
	{

	}
	public class CardInput : InputModel
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

		public CardInput()
			: base()
		{

		}
		public CardInput(Guid RID, String CardHolderName, Guid CardTypeID, String CardNumber, int Month, int Year, String CCV)
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
	public class PaymentMethodInput : CardInput
	{
		public AddressInput BillingAddress { get; set; }

		public PaymentMethodInput()
			: base()
		{

		}
		public PaymentMethodInput(AddressInput AddressInput)
			: base()
		{
			this.BillingAddress = AddressInput;
		}
	}
	public class ReviewInput : InputModel
	{

	}
	public class ProfileInput : InputModel
	{

	}
	public class SettingInput : InputModel
	{

	}

	public class ProductInput : InputModel
	{
		public Guid ItemRID { get; set; }
		public ICollection<Option> SelectedOptions { get; set; }

		public ProductInput()
			: base()
		{

		}
	}
}
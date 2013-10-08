using FFF.Models.UserSystem;
using FFF.ViewModels.Location;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.ViewModels.PaymentMethod
{
	/// <summary>
	/// This input model holds all the necessary billing information except for a billing address.
	/// </summary>
	public class PaymentMethodInput
	{
		public Guid? RID { get; set; }
		[Required]
		[Display( Name = "Credit Card Holder's Name")]
		public String CardHolderName { get; set; }
		[Required]
		[Display( Name = "Credit Card Type")]
		public Guid CardTypeID { get; set; }
		[Required]
		[DataType( DataType.CreditCard )]
		[Display( Name = "Credit Card Number")]
		[MinLength( 15 )]
		[MaxLength( 19 )]
		[CreditCard]
		public String CardNumber { get; set; }
		[Required]
		[Display( Name = "Month")]
		public int Month { get; set; }
		[Required]
		[Display( Name = "Year")]
		public int Year { get; set; }
		[Required]
		[DataType( DataType.Text )]
		[Display( Name = "Security Code (CCV)" )]
		[MinLength( 3 )]
		[MaxLength( 4 )]
		public String CCV { get; set; }
	}
	public class SelectedAddressInput : PaymentMethodInput
	{
		public Guid AddressID { get; set; }
	}
	public class NewAddressInput : PaymentMethodInput
	{
		public AddressInput Address { get; set; }
	}
}
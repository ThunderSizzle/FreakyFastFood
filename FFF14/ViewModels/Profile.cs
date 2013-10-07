using FFF.Models.UserSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FFF.ViewModels.Profile
{
	public class PersonalInput
	{
		[Required]
		[Display( Name="Account ID", Description="This is a Unique Number for your Account. This, along with your Username, helps us to ensure the identity of your account. You may be asked to provide this number when talking with our employees." )]
		public Guid RID { get; set; }
		[Required]
		[Display( Name="First Name" )]
		public String FirstName { get; set; }
		[Required]
		[Display( Name="Last Name" )]
		public String LastName { get; set; }
		[Display( Name="Account ID" )]
		public Guid GenderID { get; set; }
		[Required]
		[Display( Name="User Name" )]
		public String UserName { get; set; }

		public PersonalInput()
		{

		}
		public PersonalInput( Guid RID, String FirstName, String LastName, Gender Gender, String UserName )
		{
			this.RID = RID;
			this.FirstName = FirstName;
			this.LastName = LastName;
			this.GenderID = Gender.RID;
			this.UserName = UserName;
		}
	}
	public class PhoneInput
	{
		public Guid RID { get; set; }
		[Display( Name="Carrier", Description="We require your Carrier in order to be able to send text updates to your phone." )]
		public Guid CarrierID { get; set; }
		[Display( Name="Area Code")]
		[StringLength( 3 )]
		public String AreaCode { get; set; }
		[Display( Name="Prefix Code" )]
		[StringLength( 3 )]
		public String Prefix { get; set; }
		[Display( Name="Line Code" )]
		[StringLength( 3 )]
		public String Line { get; set; }
		[Display( Name="Receive Order Updates?", Description="Check this to receive Order Updates, both by call and text, to this default Phone Number. We will stop sending Order Updates to your previous default Phone Number." )]
		public bool ReceiveOrderUpdates { get; set; }
		[Display( Name="Set as Default Method of Email Contact", Description="If checked, all future calls and texts will go to this account, and all future orders will default to this method of contact, and will change your previous orders to this new Email, including those that were manually set." )]
		public bool defaultMethod { get; set; }

		public PhoneInput()
		{

		}
		public PhoneInput(Guid RID, Guid CarrierID, String AreaCode, String Prefix, String Line, bool ReceiveOrderUpdates, bool defaultMethod)
		{
			this.RID = RID;
			this.CarrierID = CarrierID;
			this.AreaCode = AreaCode;
			this.Prefix = Prefix;
			this.Line = Line;
			this.ReceiveOrderUpdates = ReceiveOrderUpdates;
			this.defaultMethod = defaultMethod;
		}
	}
	public class EmailInput
	{
		public Guid RID { get; set; }
		[Required]
		[Display( Name="Email Address" )]
		public String EmailAddress { get; set; }
		[Display( Name="Receive Order Updates?", Description="Check this to receive Order Updates to this default Email. We will stop sending Order Updates to your previous default Email." )]
		public bool ReceiveOrderUpdates { get; set; }
		[Display( Name="Receive Special Offers?", Description="Check this to receive Special Offers to this default Email. We will stop sending Special Offers to your previous default Email." )]
		public bool ReceiveSpecialOffers { get; set; }
		[Display( Name="Set as Default Method of Email Contact", Description="If checked, all future special offers will go to this account, and all future orders will default to this method of contact, and will change your previous orders to this new Email, including those that were manually set." )]
		public bool defaultMethod { get; set; }

		public EmailInput()
		{

		}
		public EmailInput(Guid RID, String EmailAddress, Boolean ReceiveOrderUpdates, Boolean ReceiveSpecialOffers, Boolean defaultMethod)
		{
			this.RID = RID;
			this.EmailAddress = EmailAddress;
			this.ReceiveOrderUpdates = ReceiveOrderUpdates;
			this.ReceiveSpecialOffers = ReceiveSpecialOffers;
			this.defaultMethod = defaultMethod;
		}
	}
	public class FoodInput
	{

	}
}
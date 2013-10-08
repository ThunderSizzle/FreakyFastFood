using FFF.Models;
using FFF.Models.LocationSystem;
using FFF.Models.PaymentSystem;
using FFF.Models.ProfileSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.ViewModels.Location
{
	public class AddressView
	{
		public Guid ID { get; set; }
		public String Nick { get; set; }
		public String Line1 { get; set; }
		public String Line2 { get; set; }
		public String CityTitle { get; set; }
		public String StateAbbreviation { get; set; }
		public String ZIP { get; set; }
	}
	public class AddressInput
	{
		public Guid RID { get; set; }
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
		{

		}

		public AddressInput( Guid RID, string Nick, string Line1, string Line2, string City, State State, String ZIP )
		{
			this.RID = RID;
			this.Nick = Nick;
			this.Line1 = Line1;
			this.Line2 = Line2;
			this.City = City;
			this.StateID = State.RID;
			this.ZIP = ZIP;
		}
	}
}
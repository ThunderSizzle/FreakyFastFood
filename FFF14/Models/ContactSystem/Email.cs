using FFF.Models.ProfileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models.ContactSystem
{

	public class Email : DatabaseObject
	{

		[Required]
		public string EmailAddress { get; set; }
		public virtual Profile Profile { get; set; }
		public bool OrderUpdates { get; set; }
		public bool SpecialOffers { get; set; }

		public Email()
			: base ()
		{
			this.EmailAddress = null;
		}
		public Email( String EmailAddress )
			: base()
		{
			this.EmailAddress = EmailAddress;
		}
	}
}
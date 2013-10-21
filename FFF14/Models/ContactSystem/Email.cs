using System;
using System.ComponentModel.DataAnnotations;

namespace FFF.Models
{

	public class Email : ContactDatabaseObject
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
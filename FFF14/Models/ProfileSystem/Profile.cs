using FFF.Models.ContactSystem;
using FFF.Models.FoodProviderSystem;
using FFF.Models.ImagesSystem;
using FFF.Models.LocationSystem;
using FFF.Models.PaymentSystem;
using FFF.Models.ReviewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Models.ProfileSystem
{
	//todo Comment Class
	public class Profile : ImageOwner
	{
		public virtual ICollection<Phone> Phones { get; set; }
		public virtual ICollection<Address> Addresses { get; set; }
		public virtual ICollection<Email> Emails { get; set; }
		public virtual Phone DefaultPhone { get; set; }
		public virtual Email DefaultEmail { get; set; }

		public Profile()
			: base()
		{
			this.Phones = new Collection<Phone>();
			this.Addresses = new Collection<Address>();
			this.Emails = new Collection<Email>();
		}
	}
}
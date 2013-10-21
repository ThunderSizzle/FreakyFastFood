using FFF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.Models
{
	//todo Comment Class
	public abstract class Profile : ImageOwner
	{
		public virtual ICollection<Setting> Settings { get; set; }
		public virtual ICollection<Email> Emails { get; set; }
		public virtual ICollection<Phone> Phones { get; set; }
		public virtual ICollection<Address> Addresses { get; set; }
		public virtual Phone DefaultPhone { get; set; }
		public virtual Email DefaultEmail { get; set; }
		public override bool Removeable
		{
			get
			{
				if(Settings.Count + Phones.Count+ Addresses.Count + Emails.Count > 0)
					return false;
				else
					return base.Removeable;
			}
		}
		public Profile()
			: base()
		{
			this.Settings = new Collection<Setting>();
			this.Addresses = new Collection<Address>();
			this.Emails = new Collection<Email>();
			this.Phones = new Collection<Phone>();
		}
	}
}
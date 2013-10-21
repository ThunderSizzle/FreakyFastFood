using FFF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Comment Class
	public class Restaurant : Provider
	{
		public virtual Chain Chain { get; set; }
		public virtual Address Address { get; set; }
		public virtual ICollection<Representative> Representatives { get; set; }
		public override bool Removeable
		{
			get
			{
				if(Representatives.Count > 0)
					return false;
				else
					return base.Removeable;
			}
		}
		public Restaurant()
			: base ()
		{
			this.Representatives = new Collection<Representative>();
			this.Address = new Address();
			this.Chain = new Chain();
		}
		public Restaurant(Chain Chain)
			: base()
		{
			this.Chain = Chain;
			this.Address = new Address();
			this.Representatives = new Collection<Representative>();
		}
		public Restaurant( Chain Chain, Address Address )
			: base()
		{
			this.Chain = Chain;
			this.Address = Address;
			this.Representatives = new Collection<Representative>();
		}
	}
}
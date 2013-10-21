using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Comment Class
	public abstract class Reviewable : ReviewDatabaseObject
	{
		public virtual ICollection<Review> Reviews { get; set; }
		public override bool Removeable
		{
			get
			{
				if(Reviews.Count > 0)
					return false;
				else
					return base.Removeable;
			}
		}
		public Reviewable()
			: base()
		{
			this.Reviews = new Collection<Review>();
		}
	}
}
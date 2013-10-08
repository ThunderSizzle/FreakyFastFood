using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models.ReviewSystem
{
	//todo Comment Class
	public abstract class Reviewable : DatabaseObject
	{
		public ICollection<Review> Reviews { get; set; }

		public Reviewable()
			: base()
		{
			this.Reviews = new Collection<Review>();
		}
	}
}
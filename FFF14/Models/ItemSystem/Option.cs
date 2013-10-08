using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FFF.Models.ItemSystem
{
	//todo Comment Class
	/// <summary>
	/// 
	/// </summary>
	public class Option : DatabaseObject
	{
		/// <summary>
		/// 
		/// </summary>
		public String Title { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public String Description { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public virtual Choice Choice { get; set; }
		public virtual Decimal AdditionalPrice { get; set; }
		public virtual ICollection<Product> Products { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Option()
			: base()
		{
			this.Products = new Collection<Product>();
		}
	}
}

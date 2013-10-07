using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFF.Models.ItemSystem
{
	//todo Comment Class
	public class Category : DatabaseObject
	{
		public virtual String Title { get; set; }
		public virtual ICollection<Item> Items { get; set; }
		public virtual Category Parent { get; set; }
		public virtual ICollection<Category> SubCategories { get; set; }

		public Category()
			: base()
		{

		}
		public Category ( String Title, Category Parent )
			: base()
		{
			this.Title = Title;
			this.Parent = Parent;
		}
	}
}

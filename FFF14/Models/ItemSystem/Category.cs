using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFF.Models
{
	//todo Comment Class
	public class Category : ItemDatabaseObject
	{
		public virtual String Title { get; set; }
		public virtual ICollection<Item> Items { get; set; }
		public virtual Category Parent { get; set; }
		public virtual ICollection<Category> SubCategories { get; set; }
		public override bool Removeable
		{
			get
			{
				if(Items.Count + SubCategories.Count > 0)
					return false;
				else
					return base.Removeable;
			}
		}
		public Category()
			: base()
		{

		}
		public Category( String Title )
			: base()
		{
			this.Title = Title;
		}
		public Category ( String Title, Category Parent )
			: base()
		{
			this.Title = Title;
			this.Parent = Parent;
		}
	}
}

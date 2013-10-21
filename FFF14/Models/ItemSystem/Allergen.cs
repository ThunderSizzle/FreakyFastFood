using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FFF.Models
{
	//todo Comment Class
	public class Allergen : ItemDatabaseObject
	{

		public String Name { get; set; }
		public virtual ICollection<Comestible> Comestibles { get; set; }
		public override bool Removeable
		{
			get
			{
				if(Comestibles.Count > 0)
					return false;
				else
					return base.Removeable;
			}
		}
		public Allergen()
		{
			this.Comestibles = new Collection<Comestible>();
		}
	}
}

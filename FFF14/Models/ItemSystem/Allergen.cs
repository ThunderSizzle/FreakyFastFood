using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FFF.Models.ItemSystem
{
	//todo Comment Class
	public class Allergen : DatabaseObject
	{

		public String Name { get; set; }
		public ICollection<Comestible> Comestibles { get; set; }

		public Allergen()
		{
			this.Comestibles = new Collection<Comestible>();
		}
	}
}

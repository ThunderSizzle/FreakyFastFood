using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FFF.Models.ItemSystem
{
	//todo Finish Comment Class
	/// <summary>
	/// 
	/// </summary>
	public abstract class Comestible : Item
	{
		public virtual Nutrition Nutrition { get; set; }
		/// <summary>
		/// List of Ingredients, using normal sorting (most present to least present)
		/// </summary>
		public virtual ICollection<Ingredient> Ingredients { get; set; }
		/// <summary>
		/// List of Allergens in Product
		/// </summary>
		public virtual ICollection<Allergen> Allergens { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public Comestible()
			: base ()
		{
			this.Nutrition = new Nutrition();
			this.Ingredients = new Collection<Ingredient>();
			this.Allergens = new Collection<Allergen>();
			this.Choices = new Collection<Choice>();
		}
		public Comestible(Menu Menu, String Title, String Description, Decimal Price = 0M)
			: base(Menu, Title, Description, Price)
		{
			this.Nutrition = new Nutrition();
			this.Ingredients = new Collection<Ingredient>();
			this.Allergens = new Collection<Allergen>();
			this.Choices = new Collection<Choice>();
		}

	}
}
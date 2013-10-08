using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FFF.Models.ItemSystem;

namespace FFF.ViewModels.Product
{
	public class ProductInput
	{
		public FFF.Models.ItemSystem.Item Item { get; set; }

		public ProductInput()
		{

		}
		public ProductInput( FFF.Models.ItemSystem.Item Item )
		{
			this.Item = Item;
		}
	}
}
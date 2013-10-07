using FFF.Models.ItemSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFF.ViewModels.Item
{
	public class ItemView
	{
		public Guid ItemID { get; set; }
		public String Title { get; set; }
		public String Description { get; set; }
		public Decimal Price { get; set; }
		public Menu Menu { get; set; }
	//	public Categories
	//	public Reviews
	//	public Choices
	}
}
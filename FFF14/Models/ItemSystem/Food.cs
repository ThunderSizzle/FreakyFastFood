using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FFF.Models.ItemSystem
{
	//todo Comment Class
	public class Food : Comestible
	{
		public Food()
			: base ()
		{

		}
		public Food( Menu Menu, String Title, String Description, Decimal Price = 0M )
			: base( Menu, Title, Description, Price )
		{

		}

	}
}
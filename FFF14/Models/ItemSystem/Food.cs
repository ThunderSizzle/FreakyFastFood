using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Comment Class
	public class Food : Comestible
	{
		public Food()
			: base ()
		{

		}
		public Food( Chain Chain, String Title, String Description, Decimal Price = 0M )
			: base( Chain, Title, Description, Price )
		{

		}

	}
}
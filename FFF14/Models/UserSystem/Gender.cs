using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFF.Models.UserSystem
{
	public class Gender : DatabaseObject
	{
		public String Title { get; set; }

		public Gender( )
		{

		}
		public Gender( String Title )
		{
			this.Title = Title;
		}
	}
}
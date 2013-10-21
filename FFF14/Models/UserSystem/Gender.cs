using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	public class Gender : ProfileDatabaseObject
	{
		public String Title { get; set; }
		public override bool Removeable
		{
			get
			{
				return false;
			}
		}
		public Gender( )
		{

		}
		public Gender( String Title )
		{
			this.Title = Title;
		}
	}
}
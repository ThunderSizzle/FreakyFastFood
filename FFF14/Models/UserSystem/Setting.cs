using System;

namespace FFF.Models
{
	public class Setting : ProfileDatabaseObject
	{
		public String Title { get; set; }
		public String Description { get; set; }
		public String Value { get; set; }
		public virtual Profile Profile { get; set; }
		public override bool Removeable
		{
			get
			{
				return true;
			}
		}
		public Setting()
			: base()
		{

		}
	}
}
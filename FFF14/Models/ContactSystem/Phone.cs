using System;
using System.ComponentModel.DataAnnotations;

namespace FFF.Models
{
	public class Phone : ContactDatabaseObject
	{
		[Required]
		public virtual Profile Profile { get; set; }
		public virtual Carrier Carrier { get; set; }
		[StringLength(3)]
		[Required]
		public String AreaCode { get; set; }
		[StringLength( 3 )]
		[Required]
		public String Prefix { get; set; }
		[StringLength( 4 )]
		[Required]
		public String Line { get; set; }
		public bool OrderUpdates { get; set; }
		public Phone()
			: base()
		{
			this.AreaCode = "000";
			this.Prefix = "000";
			this.Line = "0000";
		}
		public Phone( Carrier Carrier, String AreaCode, String Prefix, String Line )
			: base()
		{
			this.AreaCode = AreaCode;
			this.Prefix = Prefix;
			this.Line = Line;
		}
	}
}
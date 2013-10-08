using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models.LocationSystem
{
	//todo Comment Class
	public class State : DatabaseObject
	{
		[StringLength( 2 )]
		public String Abbreviation { get; set; }
		public String Title { get; set; }
		public Boolean Allowed { get; set; }

		public State()
			: base()
		{
		}
		public State(String Title, String Abbreviation, Boolean Allowed = false)
			: base()
		{
			this.Title = Title;
			this.Abbreviation = Abbreviation;
			this.Allowed = Allowed;
		}
	}
}
using FFF.Models.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFF.ViewModels.Review
{
	public class ReviewInput
	{
		
	}
	public class ReviewView
	{
		public FFF.Models.UserSystem.Account Poster { get; set; }
		public String Message { get; set; }
		public DateTime Timestamp { get; set; }
		public int Helpful { get; set; }
		public int Unhelpful { get; set; }
		public int Rating { get; set; }
	}
}
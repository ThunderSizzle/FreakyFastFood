using FFF.Models.UserSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models.ReviewSystem
{
	//todo Comment Class
	public class Review : DatabaseObject
	{
		public Account Poster { get; set; }
		public String Message { get; set; }
		public DateTime Timestamp { get; set; }
		public int Helpful { get; private set; }
		public int Unhelpful { get; private set; }
		public int Rating { get; set; }
		public Boolean Reported { get; set; }
		public Reviewable Reviewed { get; set; }

		public Review()
		{
			this.Reported = false;
			this.Helpful = 0;
			this.Unhelpful = 0;
			this.Timestamp = DateTime.Now;
		}
		public Review(String Message)
			: this()
		{
			this.Message = Message;
		}

		public void AddHelpful()
		{
			this.Helpful=+1;
		}
		public void AddUnHelpful()
		{
			this.Unhelpful=+1;
		}
		public int TotalRating()
		{
			return this.Helpful + this.Unhelpful;
		}

	}
}
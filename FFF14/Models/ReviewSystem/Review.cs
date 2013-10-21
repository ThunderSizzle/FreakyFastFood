using System;

namespace FFF.Models
{
	//todo Comment Class
	public class Review : ReviewDatabaseObject
	{
		public virtual Account Poster { get; set; }
		public String Message { get; set; }
		public DateTime Timestamp { get; set; }
		public int Helpful { get; private set; }
		public int Unhelpful { get; private set; }
		public int Rating { get; set; }
		public Boolean Reported { get; set; }
		public virtual Reviewable Reviewed { get; set; }
		public override bool Removeable
		{
			get
			{
				return false;
			}
		}

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
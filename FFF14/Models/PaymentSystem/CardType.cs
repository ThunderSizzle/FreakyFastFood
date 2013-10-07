using FFF.Models.LocationSystem;
using FFF.Models.UserSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FFF.Models.ProfileSystem;
using FFF.ViewModels.PaymentMethod;

namespace FFF.Models.PaymentSystem
{
	public class CardType : DatabaseObject
	{
		public String Title { get; set; }

		public CardType()
			: base()
		{

		}
		public CardType( String Title )
			: base()
		{
			this.Title = Title;
		}
	}
}
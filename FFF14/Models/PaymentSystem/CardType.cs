using FFF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	public class CardType : PaymentMethodDatabaseObject
	{
		public String Title { get; set; }
		public override bool Removeable
		{
			get
			{
				return false;
			}
		}
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
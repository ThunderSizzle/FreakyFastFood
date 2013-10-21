using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FFF.Models
{
	//todo Comment Class
	public class PaymentMethod : PaymentMethodDatabaseObject
	{
		[Required]
		[DataType( DataType.Text )]
		[MinLength( 10 )]
		[MaxLength( 20 )]
		public String CardHolderName { get; set; }
		[Required]
		[DataType( DataType.Text )]
		public virtual CardType CardType { get; private set; }
		[Required]
		[DataType( DataType.CreditCard )]
		[MinLength( 15 )]
		[MaxLength( 19 )]
		[CreditCard]
		public String CardNumber { get; set; }
		[Required]
		[DataType( DataType.Date )]
		public DateTime Expiration { get; set; }
		[Required]
		[DataType( DataType.Text )]
		[MinLength( 3 )]
		[MaxLength( 4 )]
		public String CCV { get; set; }
		public virtual Account Account { get; set; }
		public virtual Address BillingAddress { get; set; }
		public bool Expired
		{
			get
			{
				if(DateTime.Compare(DateTime.Now, this.Expiration) > 0)
					return true;
				else
					return false;
			}
		}
		public virtual ICollection<Order> Orders { get; set; }
		public override bool Removeable
		{
			get
			{
				if(Orders.Count > 0)
					return false;
				else
					return base.Removeable;
			}
		}
		public PaymentMethod ()
			: base()
		{
		}
		public PaymentMethod(String CardHolderName, CardType CardType, String CardNumber, DateTime Expiration, String CCV, Address BillingAddress)
			: base()
		{
			this.CardHolderName = CardHolderName;
			this.CardType = CardType;
			this.CardNumber = CardNumber;
			this.Expiration = Expiration;
			this.CCV = CCV;
			this.BillingAddress = BillingAddress;
			this.Orders = new Collection<Order>();
		}
		/// <summary>
		/// Tests the Card Number and identifies the CardType.
		/// </summary>
/*		public void DeriveCardType()
		{
			this.CardType = PaymentMethod.DeriveCardType(this.CardNumber);
		}
		public static String DeriveCardType(String CardNumber)
		{
			if ( CardNumber.StartsWith( "6011" ) || CardNumber.StartsWith( "65" ) )
			{
				return "Discover";
			}
			else if ( CardNumber.StartsWith( "51" ) || CardNumber.StartsWith( "52" ) || CardNumber.StartsWith( "53" ) || CardNumber.StartsWith( "54" ) || CardNumber.StartsWith( "55" ) )
			{
				return "MasterCard";
			}
			else if ( CardNumber.StartsWith( "34" ) || CardNumber.StartsWith( "37" ) )
			{
				return "AmericanExpress";
			}
			else if ( CardNumber.StartsWith( "4" ) )
			{
				return "Visa";
			}
			return null;
		}*/
	}
}
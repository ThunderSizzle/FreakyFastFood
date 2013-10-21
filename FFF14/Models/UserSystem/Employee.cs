using FFF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FFF.Models
{
	//todo Comment Class
	public class Employee : Account
	{
	//	[Required]
	//	public string Paycheck { get; set; }
		public Employee()
			: base ()
		{

		}
	}
}
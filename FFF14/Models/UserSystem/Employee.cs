using FFF.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace FFF.Models.UserSystem
{
	//todo Comment Class
	public class Employee : Account
	{
	//	[Required]
	//	public string Paycheck { get; set; }

		public Employee(RegisterViewModel Register)
			: base (Register)
		{

		}
		public Employee()
			: base ()
		{

		}
	}
}
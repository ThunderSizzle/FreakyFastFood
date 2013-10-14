using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFF.Models.UserSystem
{
	public class Connection : DatabaseObject
	{
		public virtual FFFUser User { get; set; }
		public string ConnectionID { get; set; }
		public string UserAgent { get; set; }
		public bool Connected { get; set; }
	}
}
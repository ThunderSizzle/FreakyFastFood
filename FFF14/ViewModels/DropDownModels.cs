using FFF.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFF.DropDownModels
{
	public class StateDropDownItem
	{
		public Guid RID { get; set; }
		public String Title { get; set; }

		public StateDropDownItem()
		{

		}
		public StateDropDownItem(Guid RID, String Title)
		{
			this.RID = RID;
			this.Title = Title;
		}
	}
}
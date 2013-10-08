using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace FFF.Models.ImagesSystem
{
	public class ImagePath : DatabaseObject
	{
		public String Description { get; set; }
		public ImageOwner ImageOwner { get; set; }

		public ImagePath()
		{

		}
		public ImagePath( Image Image, ImageOwner ImageOwner, String Description )
			: base()
		{
			this.Description = Description;
			this.ImageOwner = ImageOwner;
			Image.Save( "~/Content/Images/" + this.ImageOwner.RID.ToString() + "/" +  this.RID.ToString());
		}
	}
}
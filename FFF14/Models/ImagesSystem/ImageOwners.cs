using FFF.Models.ReviewSystem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;

namespace FFF.Models.ImagesSystem
{
	public class ImageOwner : Reviewable
	{
		public virtual ICollection<ImagePath> ImagePaths { get; set; }
		

		public ImageOwner()
			: base()
		{
			this.ImagePaths = new Collection<ImagePath>();
		}
	}
}
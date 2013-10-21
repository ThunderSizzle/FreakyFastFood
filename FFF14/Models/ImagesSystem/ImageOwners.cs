using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FFF.Models
{
	public class ImageOwner : Reviewable
	{
		public virtual ICollection<ImagePath> ImagePaths { get; set; }
		public override bool Removeable
		{
			get
			{
				if(ImagePaths.Count > 0)
					return false;
				else
					return true;
			}
		}

		public ImageOwner()
			: base()
		{
			this.ImagePaths = new Collection<ImagePath>();
		}
	}
}
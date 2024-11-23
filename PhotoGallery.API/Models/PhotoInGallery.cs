using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.API.Models
{
	public class PhotoInGallery
	{
		[ForeignKey("Photo")]
		public int PhotoId { get; set; }
		public Photo Photo { get; set; }

		[ForeignKey("Gallery")]
		public int GalleryId { get; set; }
		public Gallery Gallery { get; set; }
	}
}

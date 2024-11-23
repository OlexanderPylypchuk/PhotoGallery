using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.API.Models
{
	public class Gallery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[NotMapped]
		public List<PhotoInGallery> Photos { get; set; }
	}
}

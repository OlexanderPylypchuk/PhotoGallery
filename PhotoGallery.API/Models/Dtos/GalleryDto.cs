using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.API.Models.Dtos
{
	public class GalleryDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string UserId { get; set; }
		[NotMapped]
		public List<PhotoDto> Photos { get; set; }
	}
}

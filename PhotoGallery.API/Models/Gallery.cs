using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.API.Models
{
	public class Gallery
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[ForeignKey("AppUser")]
		public string UserId { get; set; }
		public AppUser AppUser { get; set; }
		public List<PhotoInGallery> Photos { get; set; }
	}
}

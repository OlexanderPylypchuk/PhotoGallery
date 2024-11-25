using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.API.Models
{
	public class Photo
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImgUrl { get; set; }
		public string ImageLocalPath { get; set; }
		public int Likes { get; set; }
		public int Dislikes { get; set; }
		[ForeignKey("AppUser")]
		public string UserId { get; set; }
		public AppUser AppUser { get; set; }
	}
}

namespace PhotoGallery.API.Models.Dtos
{
	public class PhotoDto
	{
		public int? Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string? UserId { get; set; }
		public string? ImgUrl { get; set; }
		public string? ImageLocalPath { get; set; }
		public int? Likes { get; set; }
		public int? Dislikes { get; set; }
		public IFormFile? Photo { get; set; }
	}
}

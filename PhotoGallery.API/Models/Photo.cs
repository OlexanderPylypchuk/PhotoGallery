namespace PhotoGallery.API.Models
{
	public class Photo
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImgUrl { get; set; }
		public string ImageLocalPath { get; set; }
		public string UserId { get; set; }
	}
}

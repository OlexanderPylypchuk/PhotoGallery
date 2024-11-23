using Microsoft.AspNetCore.Identity;

namespace PhotoGallery.API.Models
{
	public class AppUser : IdentityUser
	{
		public string Name { get; set; }
	}
}

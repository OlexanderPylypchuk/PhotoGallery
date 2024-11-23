using PhotoGallery.API.Models;

namespace PhotoGallery.API.Service.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(AppUser user, IEnumerable<string> roles);
	}
}

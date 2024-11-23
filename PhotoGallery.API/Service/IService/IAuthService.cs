using PhotoGallery.API.Models.Dtos;

namespace PhotoGallery.API.Service.IService
{
	public interface IAuthService
	{
		Task<LoginResponceDto> Login(LoginRequestDto loginRequest);
		Task<bool> Register(RegistrationRequestDto registrationRequest);
		Task<bool> AssignRole(string email, string role);
	}
}

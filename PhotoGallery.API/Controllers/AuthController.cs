using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.API.Models.Dtos;
using PhotoGallery.API.Service.IService;

namespace PhotoGallery.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		protected ResponceDto _responceDto;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
			_responceDto = new ResponceDto();

		}

		[HttpPost("register")]
		public async Task<ResponceDto> Register([FromBody] RegistrationRequestDto registrationRequestDto)
		{
			try
			{
				bool result = await _authService.Register(registrationRequestDto);

				if (!result)
				{
					_responceDto.Message = "Untraced error";
					return _responceDto;
				}

				_responceDto.Success = true;
			}
			catch (Exception ex)
			{
				_responceDto.Success = false;
				_responceDto.Message = ex.Message;
			}
			return _responceDto;
		}

		[HttpPost("login")]
		public async Task<ResponceDto> Login([FromBody] LoginRequestDto loginRequestDto)
		{
			var loginresponce = await _authService.Login(loginRequestDto);
			if (loginresponce.AppUser == null)
			{
				_responceDto.Success = false;
				_responceDto.Message = "Username or password is incorrect";
				return _responceDto;
			}
			_responceDto.Success = true;
			_responceDto.Result = loginresponce;
			return _responceDto;
		}

		[HttpPost("AssignRole")]
		public async Task<ResponceDto> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
		{
			bool assignRoleSuccessful = await _authService.AssignRole(registrationRequestDto.Email, registrationRequestDto.Role.ToUpper());

			if (!assignRoleSuccessful)
			{
				_responceDto.Success = false;
				_responceDto.Message = "Error occured";
				return _responceDto;
			}
			_responceDto.Success = true;
			return _responceDto;
		}
	}
}

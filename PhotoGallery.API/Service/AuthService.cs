﻿using Microsoft.AspNetCore.Identity;
using PhotoGallery.API.Data;
using PhotoGallery.API.Exceptions;
using PhotoGallery.API.Models;
using PhotoGallery.API.Models.Dtos;
using PhotoGallery.API.Service.IService;

namespace PhotoGallery.API.Service
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext _db;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_db = db;
			_jwtTokenGenerator = jwtTokenGenerator;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task<bool> AssignRole(string email, string role)
		{
			var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());
			if (user != null)
			{
				if (!_roleManager.RoleExistsAsync(role.ToLower()).GetAwaiter().GetResult())
				{
					await _roleManager.CreateAsync(new IdentityRole(role.ToLower()));
				}
				await _userManager.AddToRoleAsync(user, role);
				return true;
			}
			return false;
		}

		public async Task<LoginResponceDto> Login(LoginRequestDto loginRequest)
		{
			var user = _db.Users.FirstOrDefault(u => u.UserName.ToLower() == loginRequest.Username.ToLower());
			if (user == null)
			{
				return new LoginResponceDto();
			}

			var roles = await _userManager.GetRolesAsync(user);

			UserDto userdto = new UserDto()
			{
				Id = user.Id,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Name = user.Name
			};

			var loginresponce = new LoginResponceDto()
			{
				AppUser = userdto,
				Token = _jwtTokenGenerator.GenerateToken(user, roles)
			};

			return loginresponce;

		}

		public async Task<bool> Register(RegistrationRequestDto registrationRequest)
		{
			try
			{
				AppUser appUser = new()
				{
					UserName = registrationRequest.Email,
					Email = registrationRequest.Email,
					NormalizedEmail = registrationRequest.Email.ToUpper(),
					PhoneNumber = registrationRequest.PhoneNumber,
					Name = registrationRequest.Name,
				};
				var result = await _userManager.CreateAsync(appUser, registrationRequest.Password);
				if (result.Succeeded)
				{
					if (registrationRequest.Role != null)
					{
						await AssignRole(registrationRequest.Email, registrationRequest.Role);
					}
					return true;
				}
				throw new UserCreationException(result.Errors.FirstOrDefault().Description);
			}
			catch (UserCreationException userCreationException)
			{
				throw;
			}
			catch
			{
				return false;
			}
		}
	}
}

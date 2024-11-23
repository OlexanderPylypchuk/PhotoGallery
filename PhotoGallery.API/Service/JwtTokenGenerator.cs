using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhotoGallery.API.Models;
using PhotoGallery.API.Repository;
using PhotoGallery.API.Service.IService;
using PhotoGallery.API.Utility;

namespace PhotoGallery.API.Service
{
	public class JwtTokenGenerator : IJwtTokenGenerator
	{
		private readonly JwtOptions _options;

        public JwtTokenGenerator(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(AppUser user, IEnumerable<string> roles)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var claims = new List<Claim>
			{
				new Claim(SD.IdClaimName, user.Id),
				new Claim(JwtRegisteredClaimNames.Name, user.Name),
				new Claim(JwtRegisteredClaimNames.Email, user.Email)
			};
			claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
			var secret = Encoding.ASCII.GetBytes(_options.Key);

			var descriptor = new SecurityTokenDescriptor()
			{
				Issuer = _options.Issuer,
				Audience = _options.Audience,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256),
				Expires = DateTime.UtcNow.AddDays(1),
				Subject = new ClaimsIdentity(claims)
			};

			var token = tokenHandler.CreateToken(descriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}

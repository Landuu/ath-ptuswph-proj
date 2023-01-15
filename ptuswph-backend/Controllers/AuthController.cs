using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ptuswph_backend.Models.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ptuswph_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTSettings? _jwtSettings;

        public AuthController(IConfiguration configuration) {
            _jwtSettings = configuration.GetSection("JWT").Get<JWTSettings>();
        }

        [HttpGet]
        public IResult Post()
        {
            if (_jwtSettings == null) return Results.BadRequest();

            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Name, "JAN"),
                new(ClaimTypes.Role, "user")
            };

            var signKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new(signKey, SecurityAlgorithms.HmacSha256)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var bearer = "Bearer " + jwtToken;
            return Results.Text(bearer);
        }
    }
}

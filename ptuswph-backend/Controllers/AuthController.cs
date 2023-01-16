using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ptuswph_backend.Database;
using ptuswph_backend.Models.Dto;
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
        private readonly ApiContext _context;

        public AuthController(IConfiguration configuration, ApiContext context) {
            _jwtSettings = configuration.GetSection("JWT").Get<JWTSettings>();
            _context = context;
        }

        [HttpGet]
        public IResult Get()
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

        [HttpPost]
        public async Task<IResult> Post([FromBody] UserCredentialsDto dto)
        {
            if (_jwtSettings == null) return Results.BadRequest();

            if(string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrWhiteSpace(dto.Password)) 
                return Results.BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);
            if(user == null) return Results.BadRequest();

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if(!isPasswordValid) return Results.BadRequest();

            var claims = new Claim[]
            {
                new("login", user.Login),
                new("uid", user.Id.ToString()),
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

            var response = new LoginResponseDto()
            {
                Id = user.Id,
                Token = bearer,
                Login = user.Login
            };
            return Results.Json(response);
        }
    }
}

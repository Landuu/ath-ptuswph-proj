using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ptuswph_backend.Database;
using ptuswph_backend.Models;
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


        [HttpPost("Register")]
        public async Task<IResult> PostRegister([FromBody] UserCredentialsDto dto)
        {
            if (dto.Login.Trim().Length == 0 || dto.Password.Trim().Length == 0)
                return Results.BadRequest();

            bool isUser = await _context.Users.AnyAsync(x => x.Login == dto.Login);
            if (isUser) return Results.BadRequest();

            var user = new User()
            {
                Login = dto.Login,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Wallet = 0M
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            string token = GenerateToken(user);

            LoginResponseDto resDto = new()
            {
                Id = user.Id,
                Login = user.Login,
                Token = token
            };

            return Results.Json(resDto);
        }


        [HttpPost]
        public async Task<IResult> PostLogin([FromBody] UserCredentialsDto dto)
        {
            if (_jwtSettings == null) return Results.BadRequest();

            if(string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrWhiteSpace(dto.Password)) 
                return Results.BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);
            if(user == null) return Results.BadRequest();

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if(!isPasswordValid) return Results.BadRequest();

            string bearer = GenerateToken(user);

            var response = new LoginResponseDto()
            {
                Id = user.Id,
                Token = bearer,
                Login = user.Login
            };
            return Results.Json(response);
        }

        private string GenerateToken(User user)
        {
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
            return "Bearer " + jwtToken;
        }
    }
}

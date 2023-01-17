using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using ptuswph_backend.Database;
using ptuswph_backend.Models;
using ptuswph_backend.Utils;
using System.Security.Claims;
using System.Security.Principal;

namespace ptuswph_backend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            var users = await _context.Users.ToListAsync();
            return Results.Json(users);
        }

        [HttpPost]
        public async Task<IResult> Post(string login, string password)
        {
            var user = new User()
            {
                Login = login,
                Password = password
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Results.Ok();
        }

        [Authorize]
        [HttpGet("Wallet")]
        public async Task<IResult> GetWallet()
        {
            int? userId = User.Identity?.GetUid();
            if (userId == null) return Results.BadRequest();

            User? user = await _context.Users.FindAsync(userId);
            if (user == null) return Results.BadRequest();

            return Results.Text(user.Wallet.ToString());
        }
    }
}
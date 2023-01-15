using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ptuswph_backend.Database;
using ptuswph_backend.Models;

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
    }
}
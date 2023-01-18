using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using ptuswph_backend.Database;
using ptuswph_backend.Models;
using ptuswph_backend.Models.Dto;
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
            User? user = await GetUserFromIdentity(User.Identity);
            if (user == null) return Results.BadRequest();

            return Results.Text(user.Wallet.ToString());
        }

        [Authorize]
        [HttpPost("Wallet")]
        public async Task<IResult> PostWallet([FromBody] WalletDepositDto dto)
        {
            User? user = await GetUserFromIdentity(User.Identity);
            if (user == null) return Results.BadRequest();

            user.Wallet += dto.Ammount;
            WalletTransaction transaction = new()
            {
                UserId = user.Id,
                Ammount = dto.Ammount,
                BalanceAfter = user.Wallet,
                Description = "Wp³ata œrodków do portfela"
            };
            await _context.WalletTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            
            return Results.Ok();
        }

        [Authorize]
        [HttpGet("Transactions")]
        public async Task<IResult> GetTransactions()
        {
            User? user = await GetUserFromIdentity(User.Identity);
            if (user == null) return Results.BadRequest();

            List<WalletTransaction> transactions = await _context.WalletTransactions
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            return Results.Json(transactions);
        }

        [Authorize]
        [HttpPost("Wallet/Reset")]
        public async Task<IResult> ResetWallet()
        {
            User? user = await GetUserFromIdentity(User.Identity);
            if (user == null) return Results.BadRequest();

            user.Wallet = 0;
            await _context.WalletTransactions.Where(x => x.UserId == user.Id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return Results.Ok();
        }





        private async Task<User?> GetUserFromIdentity(IIdentity? identity)
        {
            if (identity == null) return null;
            int? userId = identity.GetUid();
            return await _context.Users.FindAsync(userId);
        }
    }
}
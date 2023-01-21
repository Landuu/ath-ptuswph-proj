using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using ptuswph_backend.Database;
using ptuswph_backend.Models;
using ptuswph_backend.Models.Dto;
using ptuswph_backend.Utils;
using System.Runtime.InteropServices;
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



        [Authorize]
        [HttpGet("Wallet")]
        public IResult GetWallet()
        {
            User? user = User.Identity?.GetUser(_context);
            if (user == null) return Results.BadRequest();

            return Results.Text(user.Wallet.ToString());
        }

        [Authorize]
        [HttpPost("Wallet")]
        public async Task<IResult> PostWallet([FromBody] WalletDepositDto dto)
        {
            User? user = User.Identity?.GetUser(_context);
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
            User? user = User.Identity?.GetUser(_context);
            if (user == null) return Results.BadRequest();

            List<WalletTransaction> transactions = await _context.WalletTransactions
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            return Results.Json(transactions);
        }

        [Authorize]
        [HttpDelete("Wallet/Reset")]
        public async Task<IResult> ResetWallet()
        {
            User? user = User.Identity?.GetUser(_context);
            if (user == null) return Results.BadRequest();

            user.Wallet = 0;
            await _context.WalletTransactions.Where(x => x.UserId == user.Id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();

            return Results.Ok();
        }

        [Authorize]
        [HttpGet("Movies")]
        public async Task<IResult> GetMovies()
        {
            User? user = User.Identity?.GetUser(_context);
            if (user == null) return Results.BadRequest();

            List<Movie> userMovies = await _context.UserMovies
                .Include(x => x.Movie)
                .Where(x => x.UserId == user.Id)
                .Select(x => x.Movie)
                .ToListAsync();

            return Results.Json(userMovies);
        }


        [Authorize]
        [HttpDelete("Movies/Reset")]
        public async Task<IResult> ResetMovies()
        {
            User? user = User.Identity?.GetUser(_context);
            if (user == null) return Results.BadRequest();

            await _context.UserMovies.Where(x => x.UserId == user.Id).ExecuteDeleteAsync();
            return Results.Ok();
        }

    }
}
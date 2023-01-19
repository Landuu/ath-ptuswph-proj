using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ptuswph_backend.Database;
using ptuswph_backend.Models;
using ptuswph_backend.Utils;

namespace ptuswph_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IWebHostEnvironment _environment;

        public MoviesController(ApiContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            var movies = await _context.Movies.OrderByDescending(x => x.Release.Substring(x.Release.Length - 4)).ToListAsync();
            return Results.Json(movies);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return Results.BadRequest();
            return Results.Json(movie);
        }

        [Authorize]
        [HttpPost("{id}/buy")]
        public async Task<IResult> BuyMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return Results.BadRequest();

            int? uid = User.Identity?.GetUid();
            if (uid == null) return Results.BadRequest();
            var user = await _context.Users.FindAsync(uid);
            if(user == null) return Results.BadRequest();

            bool isOwned = await _context.UserMovies.AnyAsync(x => x.UserId == user.Id && x.MovieId == movie.Id);
            if(isOwned) return Results.BadRequest();

            decimal walletPurchase = user.Wallet - movie.Price;
            if(walletPurchase < 0) return Results.BadRequest();

            user.Wallet = walletPurchase;
            await _context.UserMovies.AddAsync(new() { UserId = user.Id, MovieId = movie.Id });
            WalletTransaction transaction = new()
            {
                UserId = user.Id,
                Ammount = 0 - movie.Price,
                BalanceAfter = walletPurchase,
                Description = $"Zakup filmu \"{movie.Title}\""
            };
            await _context.WalletTransactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            
            return Results.Ok();
        }

        [Authorize]
        [HttpGet("{id}/Owned")]
        public async Task<IResult> IsOwnedMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id); 
            if (movie == null) return Results.BadRequest();

            int? uid = User.Identity?.GetUid();
            if (uid == null) return Results.BadRequest();
            var user = await _context.Users.FindAsync(uid);
            if (user == null) return Results.BadRequest();

            bool isOwned = await _context.UserMovies.AnyAsync(x => x.UserId == user.Id && x.MovieId == movie.Id);
            return Results.Text(isOwned.ToString());
        }


        [Authorize]
        [ResponseCache(Location = ResponseCacheLocation.None, Duration = 0, NoStore = true)]
        [HttpGet("{id}/Downloade")]
        public async Task<IResult> Download(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return Results.BadRequest();

            User? user = User.Identity?.GetUser(_context);
            if (user == null) return Results.BadRequest();

            bool isOwned = await _context.UserMovies.AnyAsync(x => x.UserId == user.Id && x.MovieId == movie.Id);
            if (!isOwned) return Results.BadRequest();

            string path = _environment.ContentRootPath + $"/database/movies/{movie.Img}";
            return Results.File(path, fileDownloadName: movie.Title + ".jpg");
        }
    }
}

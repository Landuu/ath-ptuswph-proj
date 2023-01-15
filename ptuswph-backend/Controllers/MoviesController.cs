using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ptuswph_backend.Database;

namespace ptuswph_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApiContext _context;

        public MoviesController(ApiContext context)
        {
            _context = context;
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
    }
}

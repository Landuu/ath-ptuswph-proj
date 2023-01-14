using Microsoft.AspNetCore.Mvc;

namespace ptuswph_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public UsersController(ILogger<UsersController> logger)
        {
        }

        [HttpGet]
        public async Task<IResult> Get()
        {
            return Results.Ok("GIT!");
        }
    }
}
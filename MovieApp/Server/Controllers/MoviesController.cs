using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Server.Services;
using MovieApp.Shared.Models;
using System.Threading.Tasks;


namespace MovieApp.Server.Controllers
{
    [Authorize]
    [Route("api/movies")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMoviesDbService _dbService;

        public MoviesController(ILogger<MoviesController> logger, IMoviesDbService dbService)
        {
            _logger = logger;
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return Ok(await _dbService.GetMovies());
        }

		[HttpGet("{movieId}")]
		public async Task<IActionResult> GetMovie(int movieId)
		{
			return Ok(await _dbService.GetMovie(movieId));
		}

		[HttpPost]
		public async Task<IActionResult> AddMovie([FromBody] Movie movie)
		{
			await _dbService.AddMovie(movie);
			return Ok();
		}

		[HttpPut("{movieId}")]
		public async Task<IActionResult> ModifyMovie(int movieId, [FromBody] Movie movie)
		{
			await _dbService.ModifyMovie(movieId, movie);
			return Ok();
		}

		[HttpDelete("{movieId}")]
		public async Task<IActionResult> DeleteMovie(int movieId)
		{
			await _dbService.DeleteMovie(movieId);
			return Ok();
		}
	}
}

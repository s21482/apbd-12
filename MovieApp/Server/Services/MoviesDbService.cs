using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Data;
using MovieApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Server.Services
{
	public interface IMoviesRepository
	{

	}

	public interface IMoviesDbService
	{
		Task<List<Movie>> GetMovies();
		Task AddMovie(Movie movie);
		Task<Movie> GetMovie(int movieId);
		Task ModifyMovie(int movieId, Movie movie);
		Task DeleteMovie(int movieId);
	}

	public class MoviesDbService : IMoviesDbService
	{
		private ApplicationDbContext _context;

		public MoviesDbService(ApplicationDbContext context)
		{
			_context = context;
		}

		public Task AddMovie(Movie movie)
		{
			_context.Movies.Add(movie);
			return _context.SaveChangesAsync();
		}


		public Task<Movie> GetMovie(int movieId)
		{
			return _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
		}

        public Task ModifyMovie(int movieId, Movie movie)
        {
			var movieFromDb = _context.Movies.FirstOrDefault(m => m.Id == movieId);
			movieFromDb.Title = movie.Title;
			movieFromDb.Summary = movie.Summary;
			movieFromDb.InTheaters = movie.InTheaters;
			movieFromDb.Trailer = movie.Trailer;
			movieFromDb.ReleaseDate = movie.ReleaseDate;
			movieFromDb.Poster = movie.Poster;
			movieFromDb.MoviesGenres = movie.MoviesGenres;
			movieFromDb.MoviesActors = movie.MoviesActors;
			return _context.SaveChangesAsync();
		}

		public Task DeleteMovie(int movieId)
		{
			var movie = _context.Movies.FirstOrDefault(m => m.Id == movieId);
			_context.Movies.Remove(movie);
			return _context.SaveChangesAsync();
		}

		public Task<List<Movie>> GetMovies()
		{
			return _context.Movies.OrderBy(m => m.Title).ToListAsync();
		}
	}
}

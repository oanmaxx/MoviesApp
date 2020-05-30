using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MoviesApp.Migrations;
using MoviesApp.Models;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesDbContext _context;

        public MoviesController(MoviesDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        /// <summary>
        /// Gets a list of all movies
        /// </summary>
        /// <param name="from">Filter movies added from this date(inclusive).</param>
        /// <param name="to">Filter movies add up to this date time.</param>
        /// <returns>A list of Movie objcts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies(DateTimeOffset? from = null, DateTimeOffset? to = null)
        {
            IQueryable<Movie> allMovies = _context.Movies;

            if (from != null && to != null)
            {
                allMovies = allMovies.Where(m => from <= m.DateAdded && m.DateAdded <= to);
            }
            else if (from != null)
            {
                allMovies = allMovies.Where(m => from <= m.DateAdded);
            }
            else if (to != null)
            {
                allMovies = allMovies.Where(m => m.DateAdded <= to);
            }

            var resultList = await allMovies
                .OrderByDescending(x => x.YearOfRelease)
                .ToListAsync();

            // merge all comments
            IQueryable<Comment> comments = _context.Comments;
            foreach (Movie m in resultList)
            {
                var allComments = comments.Where(a => a.Movie.Id == m.Id);
                if (allComments.Any())
                {
                    m.Comments = new List<Comment>(allComments.ToList());
                }
            }

            return resultList;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var allComments = _context.Comments.Where(a => a.Movie.Id == movie.Id);
            if (allComments.Any())
            {
                movie.Comments = new List<Comment>(allComments.ToList());
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(long id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            if (movie.Comments != null) {
                // update or delete existing comments
                var existingComments = _context.Comments.Where(a => a.Movie.Id == movie.Id);
                foreach (var c in existingComments)
                {
                    var newValue = movie.Comments.FirstOrDefault(a => a.Id == c.Id);
                    if (newValue != null)
                    {
                        newValue.Movie = movie;
                        _context.Entry(c).State = EntityState.Detached;
                        _context.Entry(newValue).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Comments.Remove(c);
                    }
                }

                // create new comments
                foreach(var c in movie.Comments.Where(a=>a.Id == 0))
                {
                    _context.Comments.Add(c);
                }
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }


            var allComments = _context.Comments.Where(a => a.Movie.Id == movie.Id);
            foreach(var c in allComments)
            {
                _context.Comments.Remove(c);
            }

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return movie;
        }

        private bool MovieExists(long id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}

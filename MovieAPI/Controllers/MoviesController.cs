using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Movie
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
    {
        return await _context.Movies.ToListAsync();
    }

    // GET: api/Movie/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _context.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

    // PUT: api/Movie/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(int? id, Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
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

    // POST: api/Movie
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // POST: api/movies/{movieId}/actors/{actorId}
    [HttpPost("{movieId}/actors/{actorId}")]
    public async Task<IActionResult> AddActorToMovie(int movieId, int actorId)
    {
        // Kontrollera att filmen finns
        var movie = await _context.Movies
            .Include(m => m.MovieActors)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie == null)
        {
            return NotFound("Movie not found");
        }

        // Kontrollera att skådespelaren finns
        var actor = await _context.Actors.FindAsync(actorId);
        if (actor == null)
        {
            return NotFound("Actor not found");
        }

        // Kontrollera att kopplingen inte redan finns
        var existing = movie.MovieActors
            .Any(ma => ma.ActorId == actorId);

        if (existing)
        {
            return BadRequest("Actor is already added to this movie");
        }

        // Skapa ny koppling
        var movieActor = new MovieActor
        {
            MovieId = movieId,
            ActorId = actorId
        };

        _context.MovieActors.Add(movieActor);
        await _context.SaveChangesAsync();

        // Returnera 201 Created eller 204 NoContent
        return CreatedAtAction(
            nameof(GetMovie),
            new { id = movieId },
            new { movieId, actorId });
    }

    // DELETE: api/Movie/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent(); // 204
    }

    private bool MovieExists(int? id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }
}

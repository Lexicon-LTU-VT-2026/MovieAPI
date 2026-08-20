using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class MovieDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public MovieDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/MovieDetail
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDetail>>> GetMovieDetail()
    {
        return await _context.MovieDetails.ToListAsync();
    }

    // GET: api/MovieDetail/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDetail>> GetMovieDetail(int id)
    {
        var moviedetail = await _context.MovieDetails.FindAsync(id);

        if (moviedetail == null)
        {
            return NotFound();
        }

        return moviedetail;
    }

    // PUT: api/MovieDetail/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovieDetail(int? id, MovieDetail moviedetail)
    {
        if (id != moviedetail.Id)
        {
            return BadRequest();
        }

        _context.Entry(moviedetail).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieDetailExists(id))
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

    // POST: api/MovieDetail
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MovieDetail>> PostMovieDetail(MovieDetail moviedetail)
    {
        _context.MovieDetails.Add(moviedetail);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMovieDetail", new { id = moviedetail.Id }, moviedetail);
    }

    // DELETE: api/MovieDetail/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovieDetail(int? id)
    {
        var moviedetail = await _context.MovieDetails.FindAsync(id);
        if (moviedetail == null)
        {
            return NotFound();
        }

        _context.MovieDetails.Remove(moviedetail);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MovieDetailExists(int? id)
    {
        return _context.MovieDetails.Any(e => e.Id == id);
    }
}

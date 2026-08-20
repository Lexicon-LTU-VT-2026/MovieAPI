using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieAPI.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ActorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Actor
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Actor>>> GetActor()
    {
        return await _context.Actors.ToListAsync();
    }

    // GET: api/Actor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Actor>> GetActor(int id)
    {
        var actor = await _context.Actors.FindAsync(id);

        if (actor == null)
        {
            return NotFound();
        }

        return actor;
    }

    // PUT: api/Actor/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActor(int? id, Actor actor)
    {
        if (id != actor.Id)
        {
            return BadRequest();
        }

        _context.Entry(actor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ActorExists(id))
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

    // POST: api/Actor
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Actor>> PostActor(Actor actor)
    {
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
    }

    // DELETE: api/Actor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActor(int? id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor == null)
        {
            return NotFound();
        }

        _context.Actors.Remove(actor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ActorExists(int? id)
    {
        return _context.Actors.Any(e => e.Id == id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers.Data;
using WebApplication1.Controllers.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly APIDbContext _context;

        public StatesController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/States
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetState()
        {
          if (_context.State == null)
          {
              return NotFound();
          }
            return await _context.State.ToListAsync();
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(string id)
        {
          if (_context.State == null)
          {
              return NotFound();
          }
            var state = await _context.State.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // PUT: api/States/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutState(string id, State state)
        {
            if (id != state.Sigla)
            {
                return BadRequest();
            }

            _context.Entry(state).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
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

        // POST: api/States
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<State>> PostState(State state)
        {
          if (_context.State == null)
          {
              return Problem("Entity set 'APIDbContext.State'  is null.");
          }
            _context.State.Add(state);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StateExists(state.Sigla))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetState", new { id = state.Sigla }, state);
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState(string id)
        {
            if (_context.State == null)
            {
                return NotFound();
            }
            var state = await _context.State.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.State.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StateExists(string id)
        {
            return (_context.State?.Any(e => e.Sigla == id)).GetValueOrDefault();
        }
    }
}

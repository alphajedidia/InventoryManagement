using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionStock.Models;
using System.Threading.Tasks;
using System.Linq;

namespace GestionStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntreesController : ControllerBase
    {
        private readonly StockContext _context;

        public EntreesController(StockContext context)
        {
            _context = context;
        }

        // GET: api/Entrees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entree>>> GetEntrees()
        {
            return await _context.Entrees.ToListAsync();
        }

        // GET: api/Entrees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entree>> GetEntree(string id)
        {
            var entree = await _context.Entrees.FindAsync(id);

            if (entree == null)
            {
                return NotFound();
            }

            return entree;
        }

        // POST: api/Entrees
        [HttpPost]
        public async Task<ActionResult<Entree>> PostEntree(Entree entree)
        {
            _context.Entrees.Add(entree);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntree", new { id = entree.NumBonEntre }, entree);
        }

        // PUT: api/Entrees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntree(string id, Entree entree)
        {
            if (id != entree.NumBonEntre)
            {
                return BadRequest();
            }

            _context.Entry(entree).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntreeExists(id))
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

        // DELETE: api/Entrees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntree(string id)
        {
            var entree = await _context.Entrees.FindAsync(id);
            if (entree == null)
            {
                return NotFound();
            }

            _context.Entrees.Remove(entree);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntreeExists(string id)
        {
            return _context.Entrees.Any(e => e.NumBonEntre == id);
        }
    }
}

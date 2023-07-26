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
    public class SortiesController : ControllerBase
    {
        private readonly StockContext _context;

        public SortiesController(StockContext context)
        {
            _context = context;
        }

        // GET: api/Sorties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sortie>>> GetSorties()
        {
            return await _context.Sorties.ToListAsync();
        }

        // GET: api/Sorties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sortie>> GetSortie(string id)
        {
            var sortie = await _context.Sorties.FindAsync(id);

            if (sortie == null)
            {
                return NotFound();
            }

            return sortie;
        }

        // POST: api/Sorties
        [HttpPost]
        public async Task<ActionResult<Sortie>> PostSortie(Sortie sortie)
        {
            _context.Sorties.Add(sortie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSortie", new { id = sortie.NumBonSortie }, sortie);
        }

        // PUT: api/Sorties/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSortie(string id, Sortie sortie)
        {
            if (id != sortie.NumBonSortie)
            {
                return BadRequest();
            }

            _context.Entry(sortie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SortieExists(id))
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

        // DELETE: api/Sorties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSortie(string id)
        {
            var sortie = await _context.Sorties.FindAsync(id);
            if (sortie == null)
            {
                return NotFound();
            }

            _context.Sorties.Remove(sortie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SortieExists(string id)
        {
            return _context.Sorties.Any(s => s.NumBonSortie == id);
        }
    }
}

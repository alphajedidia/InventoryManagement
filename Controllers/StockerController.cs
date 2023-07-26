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
    public class StockersController : ControllerBase
    {
        private readonly StockContext _context;

        public StockersController(StockContext context)
        {
            _context = context;
        }

        // GET: api/Stockers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stocker>>> GetStockers()
        {
            return await _context.Stockers.ToListAsync();
        }

        // GET: api/Stockers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stocker>> GetStocker(int id)
        {
            var stocker = await _context.Stockers.FindAsync(id);

            if (stocker == null)
            {
                return NotFound();
            }

            return stocker;
        }

        [HttpGet("StockerWithDesignation")]
        public async Task<ActionResult<IEnumerable<StockerWithDesignation>>> GetStockersWithDesignation()
        {
            string sqlQuery = "SELECT s.*, p.Designation FROM Stockers s, Produits p WHERE s.IdProduit = p.IdProduit";

            List<StockerWithDesignation> stockersWithDesignation = await _context.StockersWithDesignation.FromSqlRaw(sqlQuery).ToListAsync();

            return stockersWithDesignation;
        }
        // POST: api/Stockers
        [HttpPost]
        public async Task<ActionResult<Stocker>> PostStocker(Stocker stocker)
        {
            // Vérifier si un stocker pour le produit existant existe déjà
            var existingStocker = await _context.Stockers
                .FirstOrDefaultAsync(s => s.IdProduit == stocker.IdProduit);

            if (existingStocker != null)
            {
                // Si un stocker existe, mettre à jour la quantité
                existingStocker.QuantiteEntree += stocker.QuantiteEntree;
            }
            else
            {
                // Sinon, ajouter un nouveau stocker
                _context.Stockers.Add(stocker);
            }

            // Mettre à jour la quantité de stock dans la table Produit
            var produit = await _context.Produits.FindAsync(stocker.IdProduit);
            if (produit != null)
            {
                produit.QuantiteStock += stocker.QuantiteEntree;
            }

            // Enregistrer les changements dans la base de données
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStocker", new { id = stocker.IdStock }, stocker);
        }



        // PUT: api/Stockers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStocker(int id, Stocker stocker)
        {
            if (id != stocker.IdStock)
            {
                return BadRequest();
            }

            _context.Entry(stocker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockerExists(id))
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

        // DELETE: api/Stockers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStocker(int id)
        {
            var stocker = await _context.Stockers.FindAsync(id);
            if (stocker == null)
            {
                return NotFound();
            }

            _context.Stockers.Remove(stocker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StockerExists(int id)
        {
            return _context.Stockers.Any(s => s.IdStock == id);
        }
    }
}

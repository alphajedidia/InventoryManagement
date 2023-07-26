using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionStock.Models;
using System.Threading.Tasks;
using System.Linq;
using System;
using GestionStock.StructureJSON;

namespace GestionStock.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestockersController : ControllerBase
    {
        private readonly StockContext _context;

        public DestockersController(StockContext context)
        {
            _context = context;
        }

        // GET: api/Destockers
        [HttpGet]
        public ActionResult<IEnumerable<destockerGet>> GetDestockers()
        {
            try
            {
                var destockers = _context.Destockers
                    .Select(d => new destockerGet
                    {
                        IdDestock = d.IdDestock,
                        NumBonSortie = d.NumBonSortie,
                        IdProduit = d.IdProduit,
                        QuantiteSortie = d.QuantiteSortie
                    })
                    .ToList();

                return Ok(destockers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // GET: api/Destockers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<destockerGet>> GetDestocker(int id)
        {
            var destocker = await _context.Destockers
                .Where(d => d.IdDestock == id)
                .Select(d => new destockerGet
                {
                    IdDestock = d.IdDestock,
                    NumBonSortie = d.NumBonSortie,
                    IdProduit = d.IdProduit,
                    QuantiteSortie = d.QuantiteSortie
                })
                .FirstOrDefaultAsync();

            if (destocker == null)
            {
                return NotFound();
            }

            return destocker;
        }


        // POST: api/Destockers
        [HttpPost]
        public async Task<ActionResult<Destocker>> PostDestocker(DestockerInputModel destockerInput)
        {
            try
            {
                // Create a new Destocker object based on the input model
                Destocker destocker = new Destocker
                {
                    NumBonSortie = destockerInput.NumBonSortie,
                    IdProduit = destockerInput.IdProduit,
                    QuantiteSortie = destockerInput.QuantiteSortie
                };

                // Add the new destocker object to the database context and save changes
                _context.Destockers.Add(destocker);
                await _context.SaveChangesAsync();

                // Update the stock quantity in the Produit table
                var produit = await _context.Produits.FindAsync(destockerInput.IdProduit);
                if (produit != null)
                {
                    produit.QuantiteStock -= destockerInput.QuantiteSortie;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // If the produit does not exist, return NotFound
                    return NotFound("Product not found");
                }

                // Return the created destocker object as a response
                return CreatedAtAction("GetDestocker", new { id = destocker.IdDestock }, destocker);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // PUT: api/Destockers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestocker(int id, [FromBody] destockerGet originalJson)
        {
            // Check if the ID provided in the URL matches the ID in the JSON data
            if (id != originalJson.IdDestock)
            {
                return BadRequest();
            }

            // Find the existing Destocker entity by ID
            var destocker = await _context.Destockers.FindAsync(id);

            // Check if the Destocker entity exists
            if (destocker == null)
            {
                return NotFound();
            }

            // Update the properties of the Destocker entity
            destocker.NumBonSortie = originalJson.NumBonSortie;
            destocker.IdProduit = originalJson.IdProduit;
            destocker.QuantiteSortie = originalJson.QuantiteSortie;

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return a response with the updated Destocker entity
            return Ok(destocker);
        }




        // DELETE: api/Destockers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestocker(int id)
        {
            var destocker = await _context.Destockers.FindAsync(id);
            if (destocker == null)
            {
                return NotFound();
            }

            _context.Destockers.Remove(destocker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //query recente activity
        [HttpGet("activities")]
        public IActionResult GetActivities()
        {
            string sqlQuery = @"
        SELECT TOP 5 numBon, dateActivite, typeActivite, nomClientFournisseur
        FROM (
            SELECT numBonEntre AS numBon, dateEntree AS dateActivite, 'Entrée' AS typeActivite, nomFournisseur AS nomClientFournisseur
            FROM Entrees
            UNION ALL
            SELECT numBonSortie AS numBon, dateSortie AS dateActivite, 'Sortie' AS typeActivite, nomClient AS nomClientFournisseur
            FROM Sorties
        ) AS activites_combinees
        ORDER BY dateActivite DESC;";

            var activities = _context.Activities.FromSqlRaw(sqlQuery).ToList();

            return Ok(activities);
        }
        

        private bool DestockerExists(int id)
        {
            return _context.Destockers.Any(d => d.IdDestock == id);
        }
    }
}

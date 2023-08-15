using GestionStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MonthlyRankController : ControllerBase
{
    private readonly StockContext _context;

    public MonthlyRankController(StockContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<MonthlyRankResult>>> GetMonthlyRank()
    {
        // Execute the SQL query using FromSqlRaw method
        var sqlQuery = @"
           WITH MonthlyRank AS (
                    SELECT p.IdProduit, p.Designation, p.Descriptions, p.Img, p.QuantiteStock, 
                        COUNT(ds.IdProduit) AS TotalSorties, 
                        strftime('%Y-%m', s.DateSortie) AS [Month],
                        ROW_NUMBER() OVER (PARTITION BY strftime('%Y-%m', s.DateSortie) ORDER BY COUNT(ds.IdProduit) DESC) AS Rank
                    FROM Produits p 
                    LEFT JOIN Destockers ds ON p.IdProduit = ds.IdProduit 
                    LEFT JOIN Sorties s ON ds.NumBonSortie = s.NumBonSortie
                    GROUP BY p.IdProduit, p.Designation, p.Descriptions, p.Img, p.QuantiteStock, [Month]
                )
                SELECT IdProduit, Designation, Descriptions, Img, QuantiteStock, TotalSorties, [Month]
                FROM MonthlyRank
                WHERE Rank <= 3 AND TotalSorties > 0
                ORDER BY [Month], TotalSorties DESC;";

        // Execute the SQL query and get the results
        var monthlyRankResults = await _context.MonthlyRankResults.FromSqlRaw(sqlQuery).ToListAsync();

        // Return the results as JSON
        return monthlyRankResults;
    }
}

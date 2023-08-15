using GestionStock.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatMonthController : ControllerBase
    {
        private readonly StockContext _context;

        public StatMonthController(StockContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatisticsResult>>> GetStatistics()
        {
            var sqlQuery = @"
                            SELECT strftime('%m', e.DateEntree) AS [Month], strftime('%Y', e.DateEntree) AS [Year],
                                COUNT(e.NumBonEntre) AS NbrEntree,
                                SUM(CASE WHEN strftime('%m', s.DateSortie) = strftime('%m', e.DateEntree)
                                                AND strftime('%Y', s.DateSortie) = strftime('%Y', e.DateEntree) THEN 1 ELSE 0 END) AS NbrSortie
                            FROM Entrees e
                            LEFT JOIN Sorties s ON strftime('%m', s.DateSortie) = strftime('%m', e.DateEntree)
                                                AND strftime('%Y', s.DateSortie) = strftime('%Y', e.DateEntree)
                            GROUP BY [Year], [Month]
                            ORDER BY [Year] DESC, [Month] DESC
                            LIMIT 25 OFFSET 0;";

            var statistics = await _context.StatisticsResults.FromSqlRaw(sqlQuery).ToListAsync();

            return statistics;
        }
    }
}

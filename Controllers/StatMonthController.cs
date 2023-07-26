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
                SELECT MONTH(e.DateEntree) AS [Month], YEAR(e.DateEntree) AS [Year],
       COUNT(e.NumBonEntre) AS NbrEntree,
       SUM(CASE WHEN MONTH(s.DateSortie) = MONTH(e.DateEntree) AND YEAR(s.DateSortie) = YEAR(e.DateEntree) THEN 1 ELSE 0 END) AS NbrSortie
FROM Entrees e
LEFT JOIN Sorties s ON MONTH(s.DateSortie) = MONTH(e.DateEntree) AND YEAR(s.DateSortie) = YEAR(e.DateEntree)
GROUP BY MONTH(e.DateEntree), YEAR(e.DateEntree)
ORDER BY [Year] DESC, [Month] DESC
OFFSET 0 ROWS FETCH NEXT 25 ROWS ONLY;";

            var statistics = await _context.StatisticsResults.FromSqlRaw(sqlQuery).ToListAsync();

            return statistics;
        }
    }
}

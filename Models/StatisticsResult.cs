using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class StatisticsResult
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int NbrEntree { get; set; }
        public int NbrSortie { get; set; }
    }
}

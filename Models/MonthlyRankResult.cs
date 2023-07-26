using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class MonthlyRankResult
    {
        public int IdProduit { get; set; }
        public string Designation { get; set; }
        public string Descriptions { get; set; }
        public string Img { get; set; }
        public int QuantiteStock { get; set; }
        public int TotalSorties { get; set; }
        public int Month { get; set; }
    }
}

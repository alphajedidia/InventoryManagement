using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class StockerWithDesignation
    {
        public int IdStock { get; set; }
        public string NumBonEntre { get; set; }
        public int IdProduit { get; set; }
        public int QuantiteEntree { get; set; }
        public string Designation { get; set; }
    }
}

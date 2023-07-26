using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock
{
    public class DestockerInputModel
    {
        public string NumBonSortie { get; set; }
        public int IdProduit { get; set; }
        public int QuantiteSortie { get; set; }
    }
}

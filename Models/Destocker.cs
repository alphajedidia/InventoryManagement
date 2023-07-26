using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class Destocker
    {
        [Key]
        public int IdDestock { get; set; }

        [Required]
        public string NumBonSortie { get; set; }

        [ForeignKey("NumBonSortie")]
        public Sortie Sorties { get; set; }

        [Required]
        public int IdProduit { get; set; }

        [ForeignKey("IdProduit")]
        public Produit Produits { get; set; }

        [Required]
        public int QuantiteSortie { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class Stocker
    {
        [Key]
        public int IdStock { get; set; }

        [Required]
        public string NumBonEntre { get; set; }

        [ForeignKey("NumBonEntre")]
        public Entree Entree { get; set; }

        [Required]
        public int IdProduit { get; set; }

        [ForeignKey("IdProduit")]
        public Produit Produit { get; set; }

        [Required]
        public int QuantiteEntree { get; set; }
    }
}

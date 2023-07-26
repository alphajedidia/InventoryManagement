using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class Entree
    {
        [Key]
        public string NumBonEntre { get; set; }

        [Required]
        public DateTime DateEntree { get; set; } = DateTime.Now; // Default value for DateEntree column

        [Required]
        [MaxLength(100)]
        public string NomFournisseur { get; set; }
        public ICollection<Stocker> Stockers { get; set; }
    }
}

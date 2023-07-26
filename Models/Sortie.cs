using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class Sortie
    {
        [Key]
        public string NumBonSortie { get; set; }

        [Required]
        public DateTime DateSortie { get; set; } = DateTime.Now; // Default value for DateSortie column

        [Required]
        [MaxLength(100)]
        public string NomClient { get; set; }
        public ICollection<Destocker> Destockers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionStock.Models
{
    public class Produit
    {
        [Key]
        public int IdProduit { get; set; }

        [Required]
        [MaxLength(50)]
        public string Designation { get; set; }

        [Required]
        [MaxLength(50)]
        public string Descriptions { get; set; }

        [MaxLength(50)]
        public string Img { get; set; } = "notfound.png"; // Default value for Img column

        [Required]
        public int QuantiteStock { get; set; }
    }
}

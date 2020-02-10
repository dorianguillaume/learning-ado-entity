using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind2API_EFCode.Models
{
    public class Categorie
    {
        public Guid Id { get; set; }

        [MaxLength(40)]
        [Required]
        public string Nom { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual List<Produit> Produit { get; set; }
    }
}

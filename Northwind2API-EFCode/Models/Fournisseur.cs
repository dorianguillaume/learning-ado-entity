using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind2API_EFCode.Models
{
    public class Fournisseur
    {
        public int Id { get; set; }

        public Guid AdresseId { get; set; }

        [MaxLength(100)]
        [Required]
        public string EntrepriseNom { get; set; }

        [MaxLength(100)]
        public string ContactNom { get; set; }

        [MaxLength(40)]
        public string TitreContact { get; set; }

        [MaxLength(100)]
        public string PageAccueil { get; set; }

        public virtual List<Produit> Produit { get; set; }
        public virtual Adresse Adresse { get; set; }
    }
}

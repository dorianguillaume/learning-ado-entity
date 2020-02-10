using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind2API_EFCode.Models
{
    public class Produit
    {
        public int Id { get; set; }

        public Guid CategorieId { get; set; }

        public int FournisseurId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Nom { get; set; }
        [Column(TypeName =("money"))]
        public decimal PrixUnitaire { get; set; }

        public short UniteStock { get; set; }

        public short UniteCommande { get; set; }

        public virtual Fournisseur Fournisseur { get; set; }

        public virtual Categorie Categorie { get; set; }
    }
}

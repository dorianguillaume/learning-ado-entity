using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind2API_EFCode.Models
{
    public class Adresse
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Rue { get; set; }

        [MaxLength(40)]
        [Required]
        public string Ville { get; set; }

        [MaxLength(20)]
        [Required]
        public string CodePostal { get; set; }

        [MaxLength(40)]
        [Required]
        public string Pays { get; set; }

        [MaxLength(40)]
        public string Region { get; set; }

        [MaxLength(20)]
        public string Telephone { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Northwind2API_EFCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind2API_EFCode.Data
{
    public class Northwind2ContextEF : DbContext
    {
        public DbSet<Adresse> Adresses { get; set; }

        public DbSet<Fournisseur> Fournisseurs { get; set; }

        public DbSet<Categorie> Categories { get; set; }

        public DbSet<Produit> Produits { get; set; }

        public Northwind2ContextEF(DbContextOptions<Northwind2ContextEF> options) : base(options) { }

    }
}

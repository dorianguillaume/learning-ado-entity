using Microsoft.Extensions.Configuration;
using Northwind2API_ADO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Northwind2API_ADO.Data
{
    public class Northwind2Context
    {

        private readonly string _connect;
        public Northwind2Context(IConfiguration config)
        {
            _connect = config.GetConnectionString("Northwind2Connect");
        }


        public List<string> GetCountries()
        {
            var listCountries = new List<string>();

            var cmd = new SqlCommand();
            cmd.CommandText = @"
SELECT distinct A.country
FROM Supplier S
inner join Address A on A.AddressID=S.AddressID";

            using (var cnx = new SqlConnection(_connect))
            {

                cmd.Connection = cnx;

                cnx.Open();


                using (SqlDataReader sdr = cmd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        listCountries.Add((string)sdr["country"]);
                    }
                }
            }
            return listCountries;
        }

        public List<Supplier> GetSuppliers(string country)
        {
            var listeFournisseurs = new List<Supplier>();

            // On créé une commande et on définit le code sql à exécuter
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT SupplierID, CompanyName, HomePage
                                from Supplier S
                                inner join Address A on A.AddressID=S.AddressID
                                where A.country=@country";

            // Création d'un paramètre (on précise son type, son nom et sa valeur) 
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@country",
                Value = country
            };
            // Ajout à la collection des paramètres de la commande
            cmd.Parameters.Add(param);


            // On crée une connexion à partir de la chaîne de connexion stockée
            // dans les paramètres de l'appli
            using (var cnx = new SqlConnection(_connect))
            {

                cmd.Connection = cnx;

                cnx.Open();

                // On exécute la commande, et on lit ses résultats avec un objet SqlDataRedader
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    // On lit les lignes de résultat une par une
                    while (sdr.Read())
                    {
                        //...et pour chacune on crée un objet qu'on ajoute à la liste
                        var supplier = new Supplier();
                        supplier.SupplierId = (int)sdr["SupplierId"];
                        supplier.CompanyName = (string)sdr["CompanyName"];
                        if (sdr["HomePage"] != DBNull.Value)
                            supplier.HomePage = (string)sdr["HomePage"];

                        listeFournisseurs.Add(supplier);
                    }
                }
            }
            // Le fait d'avoir créé la connexion dans une instruction using
            // permet de fermer cette connexion automatiquement à la fin du bloc using

            return listeFournisseurs;
        }
        //
        public int GetProductsCount(string country)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"select dbo.ufn_GetProductsCount(@country)";

            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@country",
                Value = country
            };

            cmd.Parameters.Add(param);



            using (var cnx = new SqlConnection(_connect))
            {
                cmd.Connection = cnx;
                cnx.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        /*•	Dans le contexte, créer une méthode FindProducts permettant de récupérer les produits dont le nom contient 
         * une chaîne passée en paramètre. Les produits seront triés par ordre alphabétique de leur nom*/

        public List<Product> FindProducts(string demande)
        {
            var listeProduits = new List<Product>();

            // On créé une commande et on définit le code sql à exécuter
            var cmd = new SqlCommand();
            cmd.CommandText = @"SELECT ProductId, CategoryId, SupplierID, Name, UnitPrice, UnitsInStock
                                from Product P
                           where P.name like ('%'+@demande+'%')
                            order by Name";
            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@demande",
                Value = demande
            };
            // Ajout à la collection des paramètres de la commande
            cmd.Parameters.Add(param);


            // On crée une connexion à partir de la chaîne de connexion stockée
            // dans les paramètres de l'appli
            using (var cnx = new SqlConnection(_connect))
            {

                cmd.Connection = cnx;

                cnx.Open();

                // On exécute la commande, et on lit ses résultats avec un objet SqlDataRedader
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    // On lit les lignes de résultat une par une
                    while (sdr.Read())
                    {
                        //...et pour chacune on crée un objet qu'on ajoute à la liste
                        var product = new Product();
                        product.Id = (int)sdr["ProductId"];
                        product.CategoryId = (Guid)sdr["CategoryId"];
                        product.SupplierId = (int)sdr["SupplierID"];
                        product.Name = (string)sdr["Name"];
                        product.UnitPrice = (decimal)sdr["UnitPrice"];
                        product.UnitsInStock = (Int16)sdr["UnitsInStock"];

                        listeProduits.Add(product);
                    }
                }
                return listeProduits;
            }

        }
        /*•	Dans le contexte, créer une méthode GetProduct permettant de récupérer
         * le produit dont l’id est passé en paramètre*/
        public Product GetProduct(int id)
        {
            Product produit = null;

            var cmd = new SqlCommand();
            cmd.CommandText = @"select ProductId, CategoryId, SupplierID, Name, UnitPrice, UnitsInStock
                                from Product P
                           where ProductId=@id";

            var param = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = id
            };

            cmd.Parameters.Add(param);

            using (var cnx = new SqlConnection(_connect))
            {

                cmd.Connection = cnx;

                cnx.Open();

                // On exécute la commande, et on lit ses résultats avec un objet SqlDataRedader
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    // On lit les lignes de résultat une par une
                    while (sdr.Read())
                    {
                        //...et pour chacune on crée un objet qu'on ajoute à la liste
                        produit = new Product();
                        produit.Id = (int)sdr["ProductId"];
                        produit.CategoryId = (Guid)sdr["CategoryId"];
                        produit.SupplierId = (int)sdr["SupplierID"];
                        produit.Name = (string)sdr["Name"];
                        produit.UnitPrice = (decimal)sdr["UnitPrice"];
                        produit.UnitsInStock = (Int16)sdr["UnitsInStock"];
                    }
                }
                return produit;
            }
        }

        public int CreateProduct(Product product)
        {
            int produitid;
            var cmd = new SqlCommand();
            cmd.CommandText = @"insert Product(CategoryId,SupplierId,Name,UnitPrice,UnitsInStock )
                                values(@CategoryId,@SupplierId,@Name,@UnitPrice,@UnitsInStock)
                                select cast (Ident_Current('product') as int)";


            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.UniqueIdentifier,
                ParameterName = "@CategoryId",
                Value = product.CategoryId
            });
            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@SupplierId",
                Value = product.SupplierId
            });

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@Name",
                Value = product.Name
            });

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Money,
                ParameterName = "@UnitPrice",
                Value = product.UnitPrice
            });

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.SmallInt,
                ParameterName = "@UnitsInStock",
                Value = product.UnitsInStock
            });


            using (var cnx = new SqlConnection(_connect))
            {
                cnx.Open();
                cmd.Connection = cnx;
                produitid = (int)cmd.ExecuteScalar();
            }
            return produitid;
        }

        public void UpdateProduct(Product product, int id)
        {
            var cmd = new SqlCommand();
            cmd.CommandText = @"update Product set CategoryId=@CategoryId,SupplierId=@SupplierId,Name=@Name,UnitPrice=@UnitPrice,UnitsInStock=@UnitsInStock
                               where productId=@productId";

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@productId",
                Value = id
            });

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.UniqueIdentifier,
                ParameterName = "@CategoryId",
                Value = product.CategoryId
            });
            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@SupplierId",
                Value = product.SupplierId
            });

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.NVarChar,
                ParameterName = "@Name",
                Value = product.Name
            });

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Money,
                ParameterName = "@UnitPrice",
                Value = product.UnitPrice
            });

            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.SmallInt,
                ParameterName = "@UnitsInStock",
                Value = product.UnitsInStock
            });

            using (var cnx = new SqlConnection(_connect))
            {
                cnx.Open();
                cmd.Connection = cnx;
                cmd.ExecuteNonQuery();
            }
        }
        public int DeleteProduct(int idProduit)
        {
            int lines = 0;
            var cmd = new SqlCommand();
            cmd.CommandText = @"delete from Product where ProductId = @id";
            cmd.Parameters.Add(new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "@id",
                Value = idProduit
            });

            using (var cnx = new SqlConnection(_connect))
            {
                cnx.Open();
                cmd.Connection = cnx;
                lines = cmd.ExecuteNonQuery();
            }
            return lines;
        }
    }

}

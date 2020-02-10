using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Northwind2API_ADO.Data;
using Northwind2API_ADO.Models;

namespace Northwind2API_ADO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly Northwind2Context _context;
        public ProductsController(Northwind2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Product>> FindProducts([FromQuery] string demande)
        {


            if (string.IsNullOrEmpty(demande) || demande.Length < 3)
            {
                return BadRequest();
            }
            List<Product> products = _context.FindProducts(demande);

            if (products.Count == 0 /* !products.Any()*/)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}", Name = "getproduit")]
        public ActionResult<Product> GetProduct(int id)
        {
            Product produit = _context.GetProduct(id);
            if (produit is null)
            {
                return NotFound();
            }
            return Ok(produit);
        }


        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (string.IsNullOrEmpty(product.Name) || product.CategoryId == null || product.SupplierId == 0)
            {
                return BadRequest();
            }
            try
            {
                int valeur = _context.CreateProduct(product);
                return CreatedAtAction(nameof(GetProduct), new { id = valeur }, product);

            }
            catch (SqlException e)
            {
                if (e.Number == 547)
                {
                    return BadRequest("Erreur de contrainte d'intégrité");
                }
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPut]
        public ActionResult Put([FromQuery] int id, [FromBody] Product product)
        {

            if (string.IsNullOrEmpty(product.Name) || product.CategoryId == null || product.SupplierId == 0 || id < 1)
            {
                return BadRequest();
            }

            _context.UpdateProduct(product, id);
            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                int lines = _context.DeleteProduct(id);
                if (lines == 0)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (SqlException e)
            {
                if (e.Number == 547)
                {
                    return BadRequest("Erreur de contrainte d'intégrité");
                }
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
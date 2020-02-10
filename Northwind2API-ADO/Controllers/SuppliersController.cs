using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind2API_ADO.Data;
using Northwind2API_ADO.Models;

namespace Northwind2API_ADO.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly Northwind2Context _context;

        public SuppliersController(Northwind2Context context)
        {
            _context = context;
        }

        [HttpGet("pays")]
        public List<string> GetCountries()
        {
            return _context.GetCountries();
        }

        [HttpGet("pays/{country}")]
   
        public List<Supplier> GetSuppliers(string country)
        {
            return _context.GetSuppliers(country);

        }

        [HttpGet]
        public int GetProductsCount([FromQuery]string country)
        {
            return _context.GetProductsCount(country);
        }    


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind2API_EFDB.Models;

namespace Northwind2API_EFDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly Northwind2Context _context;

        public OrdersController(Northwind2Context context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders([FromQuery ]DateTime? date1, [FromQuery]DateTime? date2)
        {
            var orders = new List<Orders>();

            if (!date1.HasValue)
            {
                orders = await _context.Orders.Where(o => o.OrderDate < date2).ToListAsync();
            } 
            if (!date2.HasValue)
            {
                orders = await _context.Orders.Where(o => o.OrderDate >= date1).ToListAsync();
            }
            else
            {
                orders = await _context.Orders.Where(o => o.OrderDate > date1 && o.OrderDate < date2).ToListAsync();
            }
            return orders;
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> GetOrders(int id)
        {
            var orders = await _context.Orders.Include(o => o.OrderDetail).FirstOrDefaultAsync(o => o.OrderId == id); //Find --> Ne fonctionne pas avec Include

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }

        /// <summary>
        /// Renvoie la liste de commandes effectué par 1 utilisateur
        /// </summary>
        /// <param name="id">Id de l'utilisateur</param>
        /// <returns>Liste de commandes</returns>
        [HttpGet("customer/{id}")]

        public async Task<ActionResult<List<Orders>>> GetOrdersOfCustomers(string id)
        {
            return await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();
        }

        /// <summary>
        /// Retourne des stats pour l'ensemble des commandes d'une année
        /// </summary>
        /// <param name="year"></param>
        /// <returns>Nombre de commande / Nombre de produits / Total des commandes</returns>
        [HttpGet("stat/{year}")]
        public async Task<ActionResult<OrdersStats>> GetStats(int year)
        {
            var orders = new List<Orders>();
            int nbOrders = 0;
            int nbProducts = 0;
            float total = 0;

            orders = await _context.Orders.Where(o => o.OrderDate.Year == year).Include(o => o.OrderDetail).ToListAsync();
         

            foreach (var item in orders)
            {
                nbOrders++;

                //Boucle pour parcourir tous les détail de chaque commande
                foreach (var detail in item.OrderDetail)
                {
                    nbProducts += detail.Quantity;

                    //Calcul de la promo par commande
                    var reduc = ((float)detail.UnitPrice * detail.Quantity)*detail.Discount;
                    total += (float)detail.UnitPrice * detail.Quantity - reduc;
                }
            }

           return new OrdersStats(nbOrders, nbProducts, (int)total);

            
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders(int id, Orders orders)
        {
            if (id != orders.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        {
            _context.Orders.Add(orders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders", new { id = orders.OrderId }, orders);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> DeleteOrders(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();

            return orders;
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}

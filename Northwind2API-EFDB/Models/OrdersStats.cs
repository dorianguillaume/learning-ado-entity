using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind2API_EFDB.Models
{
    public class OrdersStats
    {
        public int OrdersCount { get; set; }

        public int ProductsCount { get; set; }

        public int Revenues { get; set; }

        public OrdersStats(int ordersCount, int productsCount, int revenues)
        {
            OrdersCount = ordersCount;
            ProductsCount = productsCount;
            Revenues = revenues;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class OrderDto
    {
        public int order_id { get; set; }
        public string OrderStatus { get; set; }
        public string date { get; set; }
        public string product_name { get; set; }
        public decimal CostPrice { get; set; }
        public int? quantity { get; set; }
        public string Name { get; set; }

        public int CustomerID { get; set; }
    }
}
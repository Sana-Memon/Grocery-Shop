using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class CartDto
    {
        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Delivery { get; set; }

        public decimal Total { get; set; }

        public IEnumerable<List> product_list { get; set; }
    }
}
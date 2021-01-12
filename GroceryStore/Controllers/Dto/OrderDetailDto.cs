using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class OrderDetailDto
    {
        public List<OrderDetail> orderDetails { get; set; }

        public CashierDetail counter_Records { get; set; }

        public order order_instance { get; set; }
    }
}
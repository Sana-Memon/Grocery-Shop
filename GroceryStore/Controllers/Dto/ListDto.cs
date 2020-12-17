using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class ListDto
    {
        public int? ProductID { get; set; }
        public int quantity { get; set; }
        public int? CustomerID { get; set; }
    }
}
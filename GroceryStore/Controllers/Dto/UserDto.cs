using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Dto
{
    public class UserDto
    {
        public User User { get; set; }
        public List<City> Cities { get; set; }
        public List<Address> Address { get; set; }
        public List<product> Products { get; set; }

        public product product { get; set; }
        public List<Category> categories { get; set; }

        public List<Customer_Complaints> Customer_Complaints { get; set; }

        public List<order> orders { get; set; }
        public List<OrderDto> OrderDto { get; set; }
        
        public IEnumerable<Cashier_Counter> Cashier_Counters { get; set; }


    }
}
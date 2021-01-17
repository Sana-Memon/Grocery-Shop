using GroceryStore.Models;
using HomeBi.Libraries.PagedList;
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
        public IPagedList<product> Products { get; set; }
        public product product { get; set; }
        public List<Category> categories { get; set; }

        public List<Customer_Complaints> Customer_Complaints { get; set; }

        public Customer_Complaints customer_Complaint { get; set; }
        public List<order> orders { get; set; }
        public List<OrderDto> OrderDto { get; set; }
        
        public List<Cashier_Counter> Cashier_Counters { get; set; }

        public Cashier_Counter cashier { get; set; }

        public OrderDetailDto order_detail_dto { get; set; }

        public Category category { get; set; }
    }
}
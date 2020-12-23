using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class UserDto
    {
        public User User { get; set; }
        public List<City> Cities { get; set; }
        public List<Address> Address { get; set; }
    }
}
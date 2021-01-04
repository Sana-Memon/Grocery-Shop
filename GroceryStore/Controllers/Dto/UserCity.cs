using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class UserCity
    {
        public UserDto user { get; set; }
        public List<City> city { get; set; }
    }
}
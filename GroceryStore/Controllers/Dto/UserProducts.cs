using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class UserProducts
    {
        public List<product> Products { get; set; }
        public User User { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class CustomerShoppingListController : Controller
    {
        // GET: CustomerShoppingList
        public ActionResult CustomerShoppingList()
        {
            return View();
        }
    }
}
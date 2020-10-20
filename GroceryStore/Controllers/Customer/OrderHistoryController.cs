using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class OrderHistoryController : Controller
    {
        // GET: OrderHistory
        public ActionResult OrderHistory()
        {
            return View();
        }
    }
}
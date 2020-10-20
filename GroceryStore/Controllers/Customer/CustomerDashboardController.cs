using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class CustomerDashboardController : Controller
    {
        // GET: CustomerDashboard
        public ActionResult CustomerDashboard()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class CustomerProfileController : Controller
    {
        // GET: CustomerProfile
        public ActionResult CustomerProfile()
        {
            return View();
        }
    }
}
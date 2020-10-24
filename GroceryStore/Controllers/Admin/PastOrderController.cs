using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Admin
{
    public class PastOrderController : Controller
    {
        // GET: OrderDispatched
        public ActionResult PastOrder()
        {
            return View();
        }
    }
}
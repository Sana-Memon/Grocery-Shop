using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Admin
{
    public class OrderQueueController : Controller
    {
        // GET: OrderQueue
        public ActionResult OrderQueue()
        {
            return View();
        }
    }
}
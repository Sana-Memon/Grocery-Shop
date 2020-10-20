using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Admin
{
    public class AdminDashboardController : Controller
    {
        // GET: AdminDashboard
        public ActionResult AdminDashboard()
        {
            return View();
        }
    }
}
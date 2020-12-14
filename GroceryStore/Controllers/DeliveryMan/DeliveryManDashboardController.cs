using GroceryStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.DeliveryMan
{
    public class DeliveryManDashboardController : Controller
    {
        // GET: DeliveryManDashboard
        public ActionResult DeliveryManDashboard()
        {
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;

            if (rolename == "" && userId == 0) return RedirectToAction("Auth", "Auth");

            if (rolename == AppRoles.DeliveryBoy)
                return View();
            return RedirectToAction("Auth", "Auth");
        }
    }
}
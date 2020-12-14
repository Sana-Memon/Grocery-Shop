using GroceryStore.Models;
using GroceryStore.Utilities;
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
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;

            if (rolename == "" && userId == 0) return RedirectToAction("Auth", "Auth");

            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename == AppRoles.Customer)
                return View(user);
            else return RedirectToAction("Auth", "Auth");
        }
    }
}
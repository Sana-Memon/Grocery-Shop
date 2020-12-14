using GroceryStore.Models;
using GroceryStore.Utilities;
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
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;


            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename == AppRoles.Admin)
                return View(user);
            return RedirectToAction("Auth", "Auth");
        }
    }
}
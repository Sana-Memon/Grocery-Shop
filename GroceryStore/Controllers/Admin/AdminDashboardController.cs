using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using GroceryStore.Services;
using GroceryStore.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Admin
{
    public class AdminDashboardController : Controller
    {

        NotificationService notificationService = new NotificationService();

        // GET: AdminDashboard
        public ActionResult AdminDashboard()
        {
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;


            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename == AppRoles.Admin) {
                AdminDashboardDetails dashboardDetails = new AdminDashboardDetails();
                var total_prod_count = db.products.Count();
                dashboardDetails.available_products = total_prod_count;
                var ended_stock_count = db.products.Where(x => x.StockAmount <= 90).Count();
                dashboardDetails.ending_products = ended_stock_count;
                return View(new UserDto() { User = user, admin_d_details = dashboardDetails});
            }
            return RedirectToAction("Auth", "Auth");
        }

        [HttpGet]
        public JsonResult GetNotifications()
        {
            int? roleId = null;
            if (Session["RoleID"] != null)
            {
                roleId = int.Parse(Session["RoleID"].ToString());
            }
            else {
                return null;
            }
                
            List<Notification> notifications = notificationService.GetNotification((int) roleId);
            return Json ( notifications.Select(x => new {
                Id  = x.Id,
                Message = x.Message,
                UserId = x.UserId,
                RoleId = x.RoleId,
                ProductNotificationId = x.ProductNotificationId,
                IsRecieved = x.IsRecieved
            }) , JsonRequestBehavior.AllowGet);
        }
    }
}
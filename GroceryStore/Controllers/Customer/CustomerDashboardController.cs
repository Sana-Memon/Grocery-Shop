﻿using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using GroceryStore.Services;
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

        NotificationService notificationService = new NotificationService();

        // GET: CustomerDashboard
        public ActionResult CustomerDashboard()
        {
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;

            if (rolename == "" && userId == 0) return RedirectToAction("Auth", "Auth");

            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename == AppRoles.Customer)
                return View(new UserDto { User = user } );
            else return RedirectToAction("Auth", "Auth");
        }

        [HttpGet]
        public JsonResult GetNotifications()
        {
            int? roleId = null;
            int? userId = null;
            if (Session["RoleID"] != null)
            {
                roleId = int.Parse(Session["RoleID"].ToString());
                userId = int.Parse(Session["userID"].ToString());
            }
            else
            {
                return null;
            }

            List<Notification> notifications = notificationService.GetNotification((int)roleId, userId);
            return Json(notifications.Select(x => new {
                Id = x.Id,
                Message = x.Message,
                UserId = x.UserId,
                RoleId = x.RoleId,
                ProductNotificationId = x.ProductNotificationId,
                IsRecieved = x.IsRecieved
            }), JsonRequestBehavior.AllowGet);
        }
    }
}
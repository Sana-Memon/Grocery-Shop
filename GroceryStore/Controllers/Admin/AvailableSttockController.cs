﻿using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using GroceryStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Admin
{
    public class AvailableSttockController : Controller
    {
        // GET: AvailableSttock
        public ActionResult AvailableSttock()
        {
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;


            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename == AppRoles.Admin)
                return View(new UserDto() { User = user });
            return RedirectToAction("Auth", "Auth");
        }
    }
}
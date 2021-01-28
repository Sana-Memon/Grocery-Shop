using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Models;
namespace GroceryStore.Controllers

{
    public class SignupController : Controller
    {
     
        [HttpGet]
        public ActionResult Signup()
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Signup(User userModel)
        {
            GroceryStoreEntities db = new GroceryStoreEntities();
            userModel.Role = 3;
            db.Users.Add(userModel);
            db.SaveChanges();

            Models.Customer cst = new Models.Customer();
            cst.UserID = userModel.UserID;
            db.Customers.Add(cst);
            db.SaveChanges();


            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Completed";
            return RedirectToAction("Auth", "Auth");
        }
    }
}
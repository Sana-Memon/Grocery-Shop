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
            using (GroceryShopModels db = new GroceryShopModels())
            {
                db.Users.Add(userModel);
                db.SaveChanges();
            }

            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Completed";
                return View("Signup", new User());
        }
    }
}
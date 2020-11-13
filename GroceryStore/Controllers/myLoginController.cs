using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class myLoginController : Controller
    {
        // GET: myLogin
        public ActionResult myLogin()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Authorise(GroceryStore.Models.User userModel)
        {


            using (GroceryShopModels db = new GroceryShopModels())
            {
                var userDetail = db.Users.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if (userDetail == null)
                {
                    userModel.LoginErrorMsg = "Invalid UserName or Password";
                }

                else
                {
                    Session["userID"] = userModel.UserID;
                    return RedirectToAction("CustomerDashboard", "CustomerDashboard");
                }

                return View("myLogin", userModel);

            }
        }
    }
}
   
using GroceryStore.Models;
using GroceryStore.Utilities;
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


            using (GroceryStoreEntities db = new GroceryStoreEntities())
            {
                var userDetail = db.Users.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if (userDetail == null)
                {
                    //userModel.LoginErrorMsg = "Invalid UserName or Password";
                }

                else
                {
                    Session["userID"] = userModel.UserID;
                    Session["roleID"] = userModel.Role;

                    var role = db.Roles.Where(x => x.Id == userModel.Role).FirstOrDefault();

                    if (role.RoleName == AppRoles.Customer)
                    {
                        return RedirectToAction("CustomerDashboard", "CustomerDashboard");
                    }
                    else if (role.RoleName == AppRoles.Admin)
                    {
                        return RedirectToAction("AdminDashboard", "AdminDashboard");
                    }

                }

                return View("myLogin", userModel);

            }
        }
    }
}
   
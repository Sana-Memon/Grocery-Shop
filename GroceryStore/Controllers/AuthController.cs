using GroceryStore.Models;
using GroceryStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Auth()
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
                    var role = db.Roles.Where(x => x.Id == userDetail.Role).FirstOrDefault();


                    Session["userID"] = userDetail.UserID;
                    if(role != null)
                    {
                        Session["RoleID"] = role.Id;
                        Session["RoleName"] = role.RoleName;
                    }

                    if (role?.RoleName == AppRoles.Customer)
                    {
                        return RedirectToAction("CustomerDashboard", "CustomerDashboard");
                    }
                    else if (role?.RoleName == AppRoles.Admin)
                    {
                        return RedirectToAction("AdminDashboard", "AdminDashboard");
                    }
                    else
                    {
                        return RedirectToAction("Home", "Index");
                    }
                }

                return RedirectToAction("Auth", "Auth", userModel); //458

            }
        }
    }
}
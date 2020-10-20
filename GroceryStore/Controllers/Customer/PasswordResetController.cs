using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class PasswordResetController : Controller
    {
        // GET: PasswordReset
        public ActionResult PasswordReset()
        {
            return View();
        }
    }
}
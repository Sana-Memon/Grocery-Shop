using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Admin
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Categories()
        {
            return View();
        }
    }
}
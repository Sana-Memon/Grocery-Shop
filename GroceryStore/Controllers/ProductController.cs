using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class ProductController : Controller
    {

        GroceryShopModels db = new GroceryShopModels();

        // GET: Product
        public ActionResult Product()
        {
            return View();
        }

        public PartialViewResult Shop()
        {
            var OurProducts = (from f in db.products
                               orderby f.product_id descending
                               select f).Take(8);
            return PartialView(OurProducts);
        }
        }
}
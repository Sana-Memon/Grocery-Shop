using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Models;
namespace GroceryStore.Controllers
{
    public class SingleProductController : Controller
    {
        // GET: SingleProduct
        GroceryStoreEntities db = new GroceryStoreEntities();
        public ActionResult SingleProduct(string ProductID)
        {
            int pid = Convert.ToInt32(ProductID);
            product obj = db.products.Find(ProductID);

            return View(obj);
        }
    }
}
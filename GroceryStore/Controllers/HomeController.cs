using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class HomeController : Controller
    {
        GroceryStoreEntities db = new GroceryStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Product()
        {
            var products = (from d in db.products 
                            select d).Take(8).ToList(); 
            return PartialView(products);
        }

        public PartialViewResult SatisfiedCustomers()
        {
            var OurSatisfiedCustomers = (from d in db.OurSatisfiedCustomers
                            select d).Take(8).ToList();
            return PartialView(OurSatisfiedCustomers);
        }

        public PartialViewResult TopRatedProduct()
        {
            var TopRatedProduct = (from d in db.products
                                         select d).Take(6).ToList();
            return PartialView(TopRatedProduct);
        }
    }
}

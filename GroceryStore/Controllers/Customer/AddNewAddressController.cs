using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class AddNewAddressController : Controller
    {
        // GET: AddNewAddress
        public ActionResult AddNewAddress()
        {
            ViewBag.saveresult = "";

            return View();  
        }

        [HttpPost]
        public ActionResult AddNewAddress(User address)
        {
            using (GroceryStoreEntities db = new GroceryStoreEntities())
            {
                db.Users.Add(address);
                db.SaveChanges();
                ViewBag.saveresult = "Address added successfully";
                ModelState.Clear();
            }
                return View(new User());
        }
    }
}
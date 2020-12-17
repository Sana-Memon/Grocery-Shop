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
        private GroceryStoreEntities db = new GroceryStoreEntities();
        // GET: AddNewAddress
        public ActionResult AddNewAddress()
        {
            ViewBag.saveresult = "";

            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();

            return View(user);  
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
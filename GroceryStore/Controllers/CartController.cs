using System;
using System.Web.Mvc;
using GroceryStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Cart()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            IEnumerable<List> lists = db.Lists.Include("product").Where(x => x.CustomerID == customer.Customer_id).ToList();

            return View(lists);
        }

        [HttpPost]
        public ActionResult SubmitList()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();
            List list = new List();
            try
            {
                int productId = Int32.Parse(Request.Form["productId"]);
                int requestCustomerId = Int32.Parse ( Request.Form["customerId"] );

                var userId = Int32.Parse ( Session["userID"]?.ToString() );
                var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault(); //.SqlQuery("SELECT Customer_id from Customers Where UserID = @Id" + p1).FirstOrDefault();
                int customerId = customer.Customer_id;



                if(requestCustomerId != customerId)
                {
                    list.ProductID = productId;
                    list.CustomerID = customerId;
                    list.quantity = 1;
                    db.Lists.Add(list);
                    db.SaveChanges();
                    return Json ( true );
                }
                else if (requestCustomerId == customerId)
                {
                    var currentList = db.Lists.Where(x => x.ProductID == productId && x.CustomerID == customerId).FirstOrDefault();
                    db.Lists.Remove(currentList);
                    db.SaveChanges();
                    return Json(false);
                }
                else
                {
                    // should throw error
                }

                return null;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult DeleteList()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            int productId = Int32.Parse(Request.Form["productId"]);

            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault(); //.SqlQuery("SELECT Customer_id from Customers Where UserID = @Id" + p1).FirstOrDefault();
            int customerId = customer.Customer_id;


            var currentList = db.Lists.Where(x => x.ProductID == productId && x.CustomerID == customerId).FirstOrDefault();
            db.Lists.Remove(currentList);
            db.SaveChanges();

            return null;
        }

        [HttpPut]
        public ActionResult UpdateList()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            int productId = Int32.Parse(Request.Form["productId"]);
            int quantity = Int32.Parse ( Request.Form["quantity"] );


            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault(); //.SqlQuery("SELECT Customer_id from Customers Where UserID = @Id" + p1).FirstOrDefault();
            int customerId = customer.Customer_id;


            var currentList = db.Lists.Where(x => x.ProductID == productId && x.CustomerID == customerId).FirstOrDefault();
            currentList.quantity = quantity;
            db.SaveChanges();

            return null;
        }

        /*public PartialViewResult GetList()
        {
           

            return PartialView(lists);

        }*/
    }
}
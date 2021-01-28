using System;
using System.Web.Mvc;
using GroceryStore.Models;
using System.Collections.Generic;
using System.Linq;
using GroceryStore.Services;
using GroceryStore.Utilities;
using GroceryStore.Controllers.Dto;

namespace GroceryStore.Controllers
{
    public class CartController : Controller
    {
        NotificationService _notificationService;
        public CartController()
        {
            _notificationService = new NotificationService();
        }

        // GET: Cart
        public ActionResult Cart()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();
            
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }

            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            IEnumerable<List> lists = db.Lists.Include("product").Where(x => x.CustomerID == customer.Customer_id).ToList();
            
            // Calculating data for Cart
            decimal price = 0;
            decimal discount = 0;

            foreach (var item in lists)
            {
                price = (decimal)(price + item.product.SellingPrice * item.quantity);
                discount = (decimal)(discount + item.product.DiscountPrice * item.quantity);
            }

            decimal delivery = 0;
            decimal total = price + delivery - discount;

            return View(new CartDto { product_list = lists, SubTotal=price, Delivery=delivery, Discount=discount, Total=total });
        }

        [HttpPost]
        public ActionResult SubmitList()
        {
            // enums
            string ADD = "ADD";
            string REMOVE = "REMOVE";
            string ERROR = "ERROR";
            string REDIRECT = "REDIRECT";

            GroceryStoreEntities db = new GroceryStoreEntities();
            List list = new List();
            try
            {
                int productId = Int32.Parse(Request.Form["productId"]);
                int requestCustomerId = Int32.Parse ( Request.Form["customerId"] );


                if (Session["userID"] == null) return Json ( REDIRECT );


                var userId = Int32.Parse ( Session["userID"]?.ToString() );


                var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault(); //.SqlQuery("SELECT Customer_id from Customers Where UserID = @Id" + p1).FirstOrDefault();
                int customerId = customer.Customer_id;

                var product = db.products.FirstOrDefault(x => x.product_id == productId);

                

                if (product.StockAmount - 1 < 10)
                {
                     int roleId = Int32.Parse(Session["RoleID"].ToString());
                    // 1 is admin role Id.
                    _notificationService.AddNotificationForAdmin(1, userId, productId);
                    return Json(ERROR);
                }

                if(requestCustomerId != customerId)
                {
                    list.ProductID = productId;
                    list.CustomerID = customerId;
                    list.quantity = 1;
                    db.Lists.Add(list);

                    product.StockAmount = product.StockAmount - 1;

                    db.SaveChanges();
                    return Json ( ADD );
                }
                else if (requestCustomerId == customerId)
                {
                    var currentList = db.Lists.Where(x => x.ProductID == productId && x.CustomerID == customerId).FirstOrDefault();
                    db.Lists.Remove(currentList);
                    db.SaveChanges();
                    return Json( REMOVE );
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

        [HttpPost]
        public ActionResult UpdateList()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            int productId = Int32.Parse(Request.Form["productId"]);
            int quantity = Int32.Parse ( Request.Form["quantity"] );


            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            int customerId = customer.Customer_id;


            var currentList = db.Lists.Where(x => x.ProductID == productId && x.CustomerID == customerId).FirstOrDefault();
            currentList.quantity = quantity;
            db.SaveChanges();
            return null;
        }

        public ActionResult PickUp()
        {
            return View();
        }
    }
}
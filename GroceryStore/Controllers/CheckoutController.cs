using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Checkout()
        {
            return View();
        }
        // POST: place an order
        public ActionResult StoreOrder()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            IList<List> lists = db.Lists.Include("product").Where(x => x.CustomerID == customer.Customer_id).ToList();

            order order = new order();

            //order.order_id = 0;
            order.customerr_id = customer.Customer_id;
            order.order_number = "123123";
            order.date = new DateTime();

            List<orderProductsPrior> orderProducts = new List<orderProductsPrior>();

            for (int i = 0; i < lists.Count; i++)
            {
                var orderProduct = new orderProductsPrior();
                orderProduct.product_id = lists[i].ProductID;
                orderProduct.quantity = lists[i].quantity;

                orderProducts.Add(orderProduct);
                db.Lists.Remove(lists[i]);
            }

            order.orderProductsPriors = orderProducts;


            db.orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Thanyou", "Thankyou");

        }
    }
}
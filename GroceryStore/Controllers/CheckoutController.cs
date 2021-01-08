using GroceryStore.Controllers.Dto;
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
        GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: Checkout
        public ActionResult Checkout()
        {
            ViewBag.saveresult = "";

            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();

            var city = db.Cities.ToList();

            var address = db.Addresses.Include("City").Where(x => x.CustomerID == id).ToList();

            return View(new UserDto() { User = user, Cities = city, Address = address });
        }


        [HttpPost]
        public ActionResult StoreOrder()
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            IList<List> lists = db.Lists.Include("product").Where(x => x.CustomerID == customer.Customer_id).ToList();
            int? addressID = (Request.Form["AddressId"] != null) ? (int?)int.Parse(Request.Form["AddressId"].ToString()) : null;

            order order = new order();

            order.customerr_id = customer.Customer_id;
            order.order_number = "123123";
            order.date = DateTime.Now;
            order.OrderStatus = "PENDING";
            order.AddressId = addressID;
            List<orderProductsPrior> orderProducts = new List<orderProductsPrior>();

            decimal total_cost = 0;
            int total_quantity = 0;

            for (int i = 0; i < lists.Count; i++)
            {
                var orderProduct = new orderProductsPrior();
                orderProduct.product_id = lists[i].ProductID;
                orderProduct.quantity = lists[i].quantity;
                total_cost = (decimal)(total_cost + (lists[i].product.SellingPrice * orderProduct.quantity) - (lists[i].product.DiscountPrice * orderProduct.quantity ));
                total_quantity = (int)(total_quantity + orderProduct.quantity);
                orderProducts.Add(orderProduct);
                db.Lists.Remove(lists[i]);
            }

            order.orderProductsPriors = orderProducts;
            order.total_cost = total_cost;
            order.total_quantity = total_quantity;

            db.orders.Add(order);
            db.SaveChanges();

            return RedirectToAction("Thanyou", "Thankyou");
        }

        [HttpPost]
        public ActionResult StoreOrderForPickup(string pickOrderTime)
        {

            GroceryStoreEntities db = new GroceryStoreEntities();
            
            var dateTime = DateTime.ParseExact(
                pickOrderTime, "H:mm", null, System.Globalization.DateTimeStyles.None
                );

            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();

            int max_count = (from k in db.Cashier_Counter select k.MaxOrderLimit - k.Current_Order_Count).Max();
            int cashier_id = (from p in db.Cashier_Counter
                              where p.MaxOrderLimit - p.Current_Order_Count == max_count
                              select p.Id).FirstOrDefault();

            

            // Retriving added products
            IList<List> lists = db.Lists.Include("product").Where(x => x.CustomerID == customer.Customer_id).ToList();


            order order = new order();

            order.customerr_id = customer.Customer_id;
            order.order_number = "123123";
            order.date = DateTime.Now;
            order.OrderStatus = "SELF PICKUP";
            List<orderProductsPrior> orderProducts = new List<orderProductsPrior>();

            decimal total_cost = 0;
            int total_quantity = 0;

            for (int i = 0; i < lists.Count; i++)
            {
                var orderProduct = new orderProductsPrior();
                orderProduct.product_id = lists[i].ProductID;
                orderProduct.quantity = lists[i].quantity;
                total_cost = (decimal)(total_cost + (lists[i].product.SellingPrice * orderProduct.quantity) - (lists[i].product.DiscountPrice * orderProduct.quantity));
                total_quantity = (int)(total_quantity + orderProduct.quantity);
                orderProducts.Add(orderProduct);
                db.Lists.Remove(lists[i]);
            }

            order.orderProductsPriors = orderProducts;
            order.total_cost = total_cost;
            order.total_quantity = total_quantity;

            db.orders.Add(order);
            db.SaveChanges();

            // Saving it in counter records
            // Creating counter record
            Counter_Records counter_Records = new Counter_Records();
            counter_Records.CashierId = cashier_id;
            counter_Records.CustomerId = customer.Customer_id;
            counter_Records.OrderId = order.order_id;
            counter_Records.TakeAwayTime = dateTime;

            db.Counter_Records.Add(counter_Records);
            db.SaveChanges();

            var cashier_instacne = db.Cashier_Counter.Where(x => x.Id == cashier_id).FirstOrDefault();
            cashier_instacne.Current_Order_Count = cashier_instacne.Current_Order_Count + 1;
            db.SaveChanges();

            return RedirectToAction("Thanyou", "Thankyou");
        }

    }
}
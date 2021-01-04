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

            //My try to get id of drop down
            Address a = new Address();
         //   a.AllAddresses = db.Addresses.ToList<Address>();



            //my try end

            var address = db.Addresses.Include("City").Where(x => x.CustomerID == id).ToList();
            // .Include(x => x. ); //.Where()

            return View(new UserDto() { User = user, Cities = city, Address = address });
        }
        
        // POST: place an order
        public ActionResult StoreOrder(UserDto MV)
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            IList<List> lists = db.Lists.Include("product").Where(x => x.CustomerID == customer.Customer_id).ToList();
            int? addressID = (Request.Form["addressId"] != null) ? (int?)int.Parse(Request.Form["addressId"].ToString()) : null;

            order order = new order();
            string SelectedValue = MV.SelectedAddress ;

            //order.order_id = 0;

            order.customerr_id = customer.Customer_id;
            order.order_number = "123123";
            order.date = DateTime.Now;
            order.OrderStatus = "PENDING";
            order.AddressId = addressID;
            List <orderProductsPrior> orderProducts = new List<orderProductsPrior>();

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Controllers.Dto;
using GroceryStore.Models;


namespace GroceryStore.Controllers.Customer
{
    public class CustomerOrderTrackController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: CustomerOrderTrack
        public ActionResult CustomerOrderTrack()
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }

            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();

            return View(new UserDto { User = user });
        }


        [HttpPost]
        public ActionResult GetCustomerOrderDetails()
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            var order_id = Int32.Parse(Request.Form["SearchQuery"]);

            var order_instance = db.orders.Where(
                x => x.order_id == order_id).FirstOrDefault();

            if (order_instance != null)
            {

                return RedirectToAction("OrderDetail", "OrderHistory", new { OrderId = order_instance.order_id });
            }

            return RedirectToAction("CustomerOrderTrack", "CustomerOrderTrack");
        }
    }
}
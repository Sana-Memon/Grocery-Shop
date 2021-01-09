using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using GroceryStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Admin
{
    public class ViewOrderQueueController : Controller
    {
        // GET: ViewOrderQueue
        public ActionResult ViewOrderQueue()
        {
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;


            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename != AppRoles.Admin)
                return RedirectToAction("Auth", "Auth");

            List<OrderDto> allOrder = (from orders in db.orders
                                       join address in db.Addresses on new { pid = orders.order_id } equals new { pid = address.Id }
                                       into yG
                                       from y1 in yG.DefaultIfEmpty()
                                       select new OrderDto()
                                       {
                                           order_id = orders.order_id,
                                           OrderStatus = orders.OrderStatus,
                                           date = ""+orders.date,
                                           CustomerID = orders.customerr_id,
                                           AddressName = orders.Address.Name,
                                           quantity = orders.total_quantity,
                                           CostPrice = (decimal)orders.total_cost,
                                           OrderType = orders.orderType
                                       }).ToList();

            return View(new UserDto { User = user, OrderDto = allOrder });
        }

        public ActionResult ChangeStatus()
        {
            string queryString = Request.QueryString["status"];
            string orderId = Request.QueryString["id"];
            int orderIdInt = 0;
            try
            {
                orderIdInt = int.Parse(orderId);
            }
            catch (Exception e)
            {
                //throw new ParserError();
            }

            GroceryStoreEntities db = new GroceryStoreEntities();

            var first = db.orders.Where(a => a.order_id == orderIdInt).FirstOrDefault();

            first.OrderStatus = queryString;

            db.SaveChanges();

            return RedirectToAction("ViewOrderQueue", "ViewOrderQueue");
        }

        public ActionResult QueueOrderDetails()
        {
            /* Require OrderId:integer as query parameter */


            // Value passed to this Api/method
            int orderId = int.Parse(Request.QueryString["OrderId"]);
            int Customer_id = int.Parse(Request.QueryString["CustomerId"]);

            // Getting customer's object
            GroceryStoreEntities db = new GroceryStoreEntities();
            var userId = Int32.Parse(Session["userID"]?.ToString());
            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            // Retrieving order details 
            var order_details = db.OrderDetails.Where(
                x => x.Customer_id == Customer_id && x.order_id == orderId
            ).ToList();

            var cashier_details = db.CashierDetails.Where(
                x => x.Customer_id == Customer_id && x.OrderId == orderId).FirstOrDefault();

            var order_detail_dto = new OrderDetailDto
            {
                orderDetails = order_details,
                counter_Records = cashier_details
            };

            return View(new UserDto { order_detail_dto = order_detail_dto, User = user });
        }
    }
}
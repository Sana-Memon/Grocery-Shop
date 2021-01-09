using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using GroceryStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class OrderHistoryController : Controller
    {
        // GET: OrderHistory
        public ActionResult OrderHistory()
        {
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;


            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename != AppRoles.Customer)
                return RedirectToAction("Auth", "Auth");

            List<OrderDto> allOrder = (from orders in db.orders
                                       join address in db.Addresses on new { pid = orders.order_id } equals new { pid = address.Id } into yG
                                       from y1 in yG.DefaultIfEmpty()
                                       where (orders.customerr_id == customer.Customer_id)
                                       select new OrderDto()
                                       {
                                           order_id = orders.order_id,
                                           OrderStatus = orders.OrderStatus,
                                           date = "" + orders.date,
                                           CustomerID = orders.customerr_id,
                                           AddressName = orders.Address.Name,
                                           quantity = orders.total_quantity,
                                           CostPrice = (decimal)orders.total_cost,
                                           OrderType = orders.orderType,
                                           status_date = orders.status_date.ToString()
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
            first.date = DateTime.Now;
            db.SaveChanges();

            return RedirectToAction("OrderHistory", "OrderHistory");
        }

 
        public ActionResult RevertOrder()
        {
            /* Require OrderId:integer as query parameter */


            // Value passed to this Api/method
            int orderId = int.Parse(Request.QueryString["OrderId"]);

            // Getting customer's object
            GroceryStoreEntities db = new GroceryStoreEntities();
            var userId = Int32.Parse(Session["userID"]?.ToString());
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();

            // ------------- Reverting order to list
            var product_ids_quantity = db.orderProductsPriors.Where(
                x => x.order_id == orderId)
                .Select(u => new { u.product_id, u.quantity})
                .ToList();

            if (product_ids_quantity != null) 
            {
                if (db.Lists.Where(x => x.CustomerID == customer.Customer_id).FirstOrDefault() != null)
                {
                    return RedirectToAction("OrderHistory", "OrderHistory");
                }
                
                foreach (var each_product in product_ids_quantity)
                {
                    List list = new List();
                    list.CustomerID = customer.Customer_id;
                    list.ProductID = each_product.product_id;
                    list.quantity = each_product.quantity;
                    db.Lists.Add(list);
                }

                var counter_record_to_remove = db.Counter_Records.Where(
                    x => x.CustomerId == customer.Customer_id)
                    .Where(x => x.OrderId == orderId).FirstOrDefault();

                var decrease_counter_current_count = db.Cashier_Counter.Where(
                    x => x.Id == counter_record_to_remove.CashierId
                    ).FirstOrDefault();

                var orderToRemove = db.orders.Where(
                    x => x.order_id == orderId)
                    .Where(x => x.customerr_id == customer.Customer_id).FirstOrDefault();

                // Decreasing current order count of Cashier_Counter
                decrease_counter_current_count.Current_Order_Count = decrease_counter_current_count.Current_Order_Count - 1;

                // Finally deleting records
                db.orderProductsPriors.RemoveRange(
                    db.orderProductsPriors.Where(x => x.order_id == orderId)
                    );
                db.Counter_Records.Remove(counter_record_to_remove);
                db.orders.Remove(orderToRemove);
                db.SaveChanges();
            }
            return RedirectToAction("OrderHistory", "OrderHistory");
        }
    
        public ActionResult OrderDetail()
        {
            /* Require OrderId:integer as query parameter */


            // Value passed to this Api/method
            int orderId = int.Parse(Request.QueryString["OrderId"]);

            // Getting customer's object
            GroceryStoreEntities db = new GroceryStoreEntities();
            var userId = Int32.Parse(Session["userID"]?.ToString());
            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();
            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();

            // Retrieving order details 
            var order_details = db.OrderDetails.Where(
                x => x.Customer_id == customer.Customer_id && x.order_id == orderId
            ).ToList();

            var cashier_details = db.CashierDetails.Where(
                x => x.Customer_id == customer.Customer_id && x.OrderId == orderId).FirstOrDefault();

            var order_detail_dto = new OrderDetailDto { 
                orderDetails = order_details , counter_Records = cashier_details};

            return View(new UserDto {order_detail_dto = order_detail_dto , User = user});
        }
    }
}
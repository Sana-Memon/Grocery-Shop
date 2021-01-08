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
                                           Name = orders.Address.Name,
                                           quantity = orders.total_quantity,
                                           CostPrice = (decimal)orders.total_cost
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

    }
}
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

            if (rolename != AppRoles.Customer)
                return RedirectToAction("Auth", "Auth");

            var orders = db.orders.Join(db.orderProductsPriors,
                                                    x => x.order_id, y => y.order_id, (x, y) => new { x, y })
                .Join(db.products, y => y.y.product_id, b => b.product_id, (x, y) => new { 
                    x.x.order_id,
                    x.x.OrderStatus,
                    x.x.date,
                    y.product_name,
                    y.CostPrice,
                    x.y.quantity,
                    x.x.AddressId,
                    x.x.customerr_id
                })
                .Select(x => new OrderDto()
                {
                    order_id = x.order_id,
                    OrderStatus = x.OrderStatus,
                    date = x.date,
                    product_name = x.product_name,
                    CostPrice = x.CostPrice,
                    quantity = x.quantity,
                    Name = x.product_name,
                    CustomerID = x.customerr_id
                })
                /*.Join(db.Addresses, x => x.AddressId, y => y.Id, (x, y) => new OrderDto()
                {
                    order_id = x.order_id,
                    OrderStatus = x.OrderStatus,
                    date = x.date,
                    product_name = x.product_name,
                    CostPrice = x.CostPrice,
                    quantity = x.quantity,
                    Name = y.Name,
                    CustomerID = x.customerr_id
                })*/
                .ToList() ;

            var customers = db.Users.Include("Customers").Where(x => x.UserID == userId).ToList();

            List<OrderDto> final = new List<OrderDto>();

            if (rolename == AppRoles.Admin)
            {
                return View(new UserDto { User = user, OrderDto = orders });
            }

            for(int i = 0; i < customers.Count; i++)
            {
                for (int j = 0; j < orders.Count; j++)
                {
                    if (customers[i].Customers.ToList()[0].Customer_id == orders[i].CustomerID)
                        final.Add(orders[i]);
                }
            }

            return View(new UserDto { User = user, OrderDto = final } );

        }
    }
}
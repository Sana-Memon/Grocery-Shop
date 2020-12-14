using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.DeliveryMan
{
    public class NewOrderController : Controller
    {
        // GET: NewOrder
        public ActionResult NewOrder()
        {
            return View();
        }

        // POST: place an order
        /*public ActionResult StoreOrder()
        {
            var Form = Request.Form;

            return null;
        }*/
    }
}
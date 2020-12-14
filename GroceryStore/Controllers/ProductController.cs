using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class ProductController : Controller
    {

        GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: Product
        public ActionResult Product()
        {   

            return View();
        }

        public PartialViewResult Shop()
        {
            //int Page_No = 1;

            //int Size_Of_Page = 6;
            //int No_Of_Page = (Page_No ?? 1);
            //return View(product.ToPagedList(No_Of_Page, Size_Of_Page));
           
            int? userId = null;
            try
            {
                userId = Int32.Parse(Session["userID"]?.ToString());
            }
            catch (Exception e)
            {
            }

            var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            int? customerId = customer?.Customer_id;


            var appProducts = db.Lists.Where(x => x.CustomerID == customerId);


            var OurProducts = (from product in db.products
                               join list in db.Lists on new { pid = product.product_id } equals new { pid = list.ProductID } into yG
                               from y1 in yG.DefaultIfEmpty()
                               where (y1.CustomerID == customerId || (y1.ProductID == null && y1.CustomerID == null)) 
                               orderby product.product_id descending
                               select new ProductDto {
                                    product_id = product.product_id,
                                    product_name = product.product_name,
                                    category_id = product.category_id,
                                    ImageFilePath = product.ImageFilePath,
                                    SellingPrice = product.SellingPrice,
                                    CostPrice = product.CostPrice,
                                    DiscountPrice = product.DiscountPrice,
                                    FullDescription = product.FullDescription,
                                    MartLocation = product.MartLocation,
                                    ImageFilePath2 = product.ImageFilePath2,
                                    customerId = ( y1 != null) ? y1.CustomerID : 0,
                               }).Take(8).ToArray();



            return PartialView(OurProducts);
        }
    }
}
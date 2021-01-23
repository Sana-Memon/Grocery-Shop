using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class ProductController : Controller
    {

        GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: Product
        public ActionResult Product(int? Page_No)
        {
            // ViewBag.Category = new SelectList(db.Categories, "category_id", "category1");

          
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
                               select new ProductDto
                               {
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
                                   customerId = (y1 != null) ? y1.CustomerID : 0,
                               }).Take(20).ToList();


            int Size_Of_Page = 8;
            int No_Of_Page = (Page_No ?? 1);


            return View(OurProducts.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        public ActionResult pvMLComponent(int customerID)
        {
            List<product> _RecommendedList = MLComponent_ProductPreferences(customerID);

            return PartialView(_RecommendedList);
        }


        public List<product> MLComponent_ProductPreferences(int CustomerID)
        {
            ViewBag.AlgorithmMode = "Product Preferences Algorithm";

            DataTable dt = new DataTable();
            dt.Columns.Add("product_id");
            dt.Columns.Add("recommendedProducts");
            dt.Columns.Add("score");
            dt.Columns.Add("rank");

            DataTable dtProducts = new DataTable();
            dtProducts.Columns.Add("product_id");
            dtProducts.Columns.Add("recommendedProducts");
            dtProducts.Columns.Add("score");
            dtProducts.Columns.Add("rank");

            List<product> _RecommendedProductsList = new List<product>();

            var CurrentUser = (from x in db.Customers
                               where x.Customer_id == CustomerID
                               select x).FirstOrDefault();

            ViewBag.FullName = "Shareef Ahmed";  // CurrentUser.FullName

            var productList = db.Lists
                                .Where(x => x.CustomerID == CustomerID)
                                .Select(x => x.ProductID)
                                .ToList();


            dt = ReadCsvFile(Server.MapPath("~") + "MLComponent\\ItemRecommendationsKB.csv");

            foreach (var item in productList)
            {
                DataRow[] result = dt.Select("trim(product_id) = '" + item.ToString() + "'");
                foreach (DataRow row in result)
                {
                    dtProducts.ImportRow(row);
                }
            }

            foreach (DataRow row in dtProducts.Rows)
            {
                int? prodID = int.Parse(row[1].ToString());
                product ProductItem = db.products.Where(d => d.product_id == prodID).FirstOrDefault();

                List todolst = db.Lists.Where(x => x.CustomerID == CustomerID && x.ProductID == prodID).FirstOrDefault();
                
                if (todolst != null)
                {
                    // Todo : Recommendation Already Added
                }
                else
                {
                    _RecommendedProductsList.Add(ProductItem);
                }
            }

            return _RecommendedProductsList;
        }

        public DataTable ReadCsvFile(string filePath)
        {
            string Fulltext = String.Empty;
            DataTable dtCsv = new DataTable();

            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    for (int i = 0; i < rows.Count() - 1; i++)
                    {
                        string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                        {
                            if (i == 0)
                            {
                                for (int j = 0; j < rowValues.Count(); j++)
                                {
                                    dtCsv.Columns.Add(rowValues[j]); //add headers  
                                }
                            }
                            else
                            {
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                {
                                    dr[k] = rowValues[k].ToString();
                                }
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                }
            }

            return dtCsv;
        }

        public PartialViewResult Shop()
        {
            //int Page_No = 1;

            //int Size_Of_Page = 6;
            //int No_Of_Page = (Page_No ?? 1);
            //return View(product.ToPagedList(No_Of_Page, Size_Of_Page));

            //int? userId = null;
            //try
            //{
            //    userId = Int32.Parse(Session["userID"]?.ToString());
            //}
            //catch (Exception e)
            //{
            //}

            //var customer = db.Customers.Where(x => x.UserID == userId).FirstOrDefault();
            //int? customerId = customer?.Customer_id;


            //var appProducts = db.Lists.Where(x => x.CustomerID == customerId);


            //var OurProducts = (from product in db.products
            //                   join list in db.Lists on new { pid = product.product_id } equals new { pid = list.ProductID } into yG
            //                   from y1 in yG.DefaultIfEmpty()
            //                   where (y1.CustomerID == customerId || (y1.ProductID == null && y1.CustomerID == null)) 
            //                   orderby product.product_id descending
            //                   select new ProductDto {
            //                        product_id = product.product_id,
            //                        product_name = product.product_name,
            //                        category_id = product.category_id,
            //                        ImageFilePath = product.ImageFilePath,
            //                        SellingPrice = product.SellingPrice,
            //                        CostPrice = product.CostPrice,
            //                        DiscountPrice = product.DiscountPrice,
            //                        FullDescription = product.FullDescription,
            //                        MartLocation = product.MartLocation,
            //                        ImageFilePath2 = product.ImageFilePath2,
            //                        customerId = ( y1 != null) ? y1.CustomerID : 0,
            //                   }).Take(8).ToArray();



            //return PartialView(OurProducts);

            return PartialView();
        }

        [HttpPost]
        public ActionResult Product(string searchText, string Category, int? Page_No)
        {
            // ViewBag.Category = new SelectList(db.Categories, "category_id", "category1");


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
                               select new ProductDto
                               {
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
                                   customerId = (y1 != null) ? y1.CustomerID : 0,
                               }).Take(8).ToList();

            int byPrice = 0;
 

            try
            {
                byPrice = Convert.ToInt32(searchText);
            }
            catch(Exception e)
            {
                 
            }

            if (searchText != null)
            {
                OurProducts = (from product in db.ProductCategories
                               where (product.product_name.Contains(searchText) || product.category.Contains(searchText) || product.CostPrice == byPrice)
                               select new ProductDto
                               {
                                   product_id = product.product_id,
                                   product_name = product.product_name,
                                   category_id = product.category_id,
                                   ImageFilePath = product.ImageFilePath,
                                   SellingPrice = product.SellingPrice,
                                   CostPrice = product.CostPrice,
                                   DiscountPrice = product.DiscountPrice,
                                   FullDescription = product.FullDescription,
                                   MartLocation = product.MartLocation,
                                   ImageFilePath2 = product.ImageFilePath2
                               }).ToList();
            }


            int Size_Of_Page = 8;
            int No_Of_Page = (Page_No ?? 1);

            return View(OurProducts.ToPagedList(No_Of_Page, Size_Of_Page));
        }
    }
}
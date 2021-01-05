using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Models;
using GroceryStore.Models.ViewModel;
namespace GroceryStore.Controllers
{
    public class SingleProductController : Controller
    {
        // GET: SingleProduct
        GroceryStoreEntities db = new GroceryStoreEntities();
        public ActionResult SingleProduct(string product_id)
        {
          int pid = Convert.ToInt32(product_id);
            product obj = db.products.Find(pid);

            return View(obj);
        }

        public ActionResult Comments()
        {
            IEnumerable<ProductViewModel> ListOfProduct = (from objProduct in db.products
                                                           select new ProductViewModel()
                                                           {
                                                               FullDescription = objProduct.FullDescription,
                                                               product_id= objProduct.product_id,
                                                               product_name = objProduct.product_name

                                                           }
                                                           ).ToList();
            return View(ListOfProduct);
        }

        public ActionResult ShowComment(int product_id)
        {
            IEnumerable<CommentViewModel> ListOfComment = (from objComment in db.Review_Ratings
                                                           where objComment.product_id == product_id
                                                           select new CommentViewModel()
                                                           {
                                                               R_Id = objComment.R_Id,
                                                               product_id = objComment.product_id,
                                                               Rate = objComment.Rate,
                                                               Review = objComment.Review,
                                                               ImageFilePath = objComment.ImageFilePath

                                                           }
                                                          ).ToList();
            ViewBag.product_id = product_id;
            return View(ListOfComment);
        }

        [HttpPost]
        public ActionResult AddComment(int product_id, int rating,string productComment, string name)
        {
            Review_Ratings model = new Review_Ratings();
            model.product_id = product_id;
            model.Rate = rating;
            model.Review = productComment;
          //  model.Name = name;
            db.Review_Ratings.Add(model);
            db.SaveChanges();
            return RedirectToAction("Product", "Product");
        }


    }
}
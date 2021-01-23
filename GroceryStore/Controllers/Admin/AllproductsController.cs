using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using GroceryStore.Services;
using HomeBi.Libraries.PagedList;

namespace GroceryStore.Controllers
{
    public class AllproductsController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();
        private NotificationService notificationService = new NotificationService();

        // GET: Allproducts
        public ActionResult Index(int? Page_No)
        {
            if(Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            var products = db.products.Include(p => p.Category).Include(p => p.SKU);
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            int Size_Of_Page = 8;
            int No_Of_Page = (Page_No ?? 1);

            return View(new UserDto { Products = products.ToList().ToPagedList(No_Of_Page, Size_Of_Page), User = user }) ;
        }

        // GET: Allproducts/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var products = db.products.Include(p => p.Category).Include(p => p.SKU);
            id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();

            return View(new UserDto { product = product, User = user });
           // return View(product);
        }

        // GET: Allproducts/Create
        public ActionResult Create()

        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category1");
            ViewBag.SkuId = new SelectList(db.SKUs, "SkuId", "Description");
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();

            return View(new UserDto { User=user});
        }

        // POST: Allproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_name,ImageFilePath,SellingPrice,CostPrice,DiscountPrice,FullDescription,StockAmount,MartLocation,ImageFilePath2")] product product)
        {
            product.category_id = Int32.Parse(Request.Form["category_id"]);
            product.SkuId = Int32.Parse(Request.Form["SkuId"]);
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }

            db.products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Allproducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }

            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category1", product.category_id);
            ViewBag.SkuId = new SelectList(db.SKUs, "SkuId", "Description", product.SkuId);
            return View(new UserDto { product = product, User=user});
        }

        // POST: Allproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,category_id,ImageFilePath,SellingPrice,CostPrice,DiscountPrice,FullDescription,StockAmount,SkuId,MartLocation,ImageFilePath2")] product product)
        {

            product.category_id = Int32.Parse(Request.Form["category_id"]);
            product.SkuId = Int32.Parse(Request.Form["SkuId"]);
            if (Session["userID"] == null) return RedirectToAction("Auth", "Auth"); 
           
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;
                notificationService.AddNotificationForCustomer(product.product_id);

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // GET: Allproducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }

            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(new UserDto { User= user, product=product});
        }

        // POST: Allproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            try { 
                db.products.Remove(product);
                db.SaveChanges();
            }
            catch
            {
                // Do nothing
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

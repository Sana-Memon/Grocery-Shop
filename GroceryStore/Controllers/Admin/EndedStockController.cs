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

namespace GroceryStore.Controllers.Admin
{
    public class EndedStockController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: EndedStock
        public ActionResult Index()
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            var products = db.products.Include(p => p.Category).Include(p => p.SKU).Where(m => m.StockAmount <= 40);
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();

            return View(new UserDto { Products = products.ToList(), User = user });
        }

        // GET: EndedStock/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: EndedStock/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category1");
            ViewBag.SkuId = new SelectList(db.SKUs, "SkuId", "Description");
            return View();
        }

        // POST: EndedStock/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,category_id,ImageFilePath,SellingPrice,CostPrice,DiscountPrice,FullDescription,StockAmount,SkuId,MartLocation,ImageFilePath2")] product product)
        {
            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category1", product.category_id);
            ViewBag.SkuId = new SelectList(db.SKUs, "SkuId", "Description", product.SkuId);
            return View(product);
        }

        // GET: EndedStock/Edit/5
        public ActionResult Edit(int? id)
        {
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
            return View(product);
        }

        // POST: EndedStock/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,category_id,ImageFilePath,SellingPrice,CostPrice,DiscountPrice,FullDescription,StockAmount,SkuId,MartLocation,ImageFilePath2")] product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "category_id", "category1", product.category_id);
            ViewBag.SkuId = new SelectList(db.SKUs, "SkuId", "Description", product.SkuId);
            return View(product);
        }

        // GET: EndedStock/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: EndedStock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
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

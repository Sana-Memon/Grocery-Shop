using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GroceryStore.Models;

namespace GroceryStore.Controllers
{
    public class Delivery_boyController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: Delivery_boy
        public ActionResult Index()
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            var delivery_boy = db.Delivery_boy.Include(d => d.User);
            return View(delivery_boy.ToList());
        }

        // GET: Delivery_boy/Details/5
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
            Delivery_boy delivery_boy = db.Delivery_boy.Find(id);
            if (delivery_boy == null)
            {
                return HttpNotFound();
            }
            return View(delivery_boy);
        }

        // GET: Delivery_boy/Create
        public ActionResult Create()
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserID", "FullName");
            return View();
        }

        // POST: Delivery_boy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,CNIC,Address,ContactNo")] Delivery_boy delivery_boy)
        {
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            if (ModelState.IsValid)
            {
                db.Delivery_boy.Add(delivery_boy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserID", "FullName", delivery_boy.UserId);
            return View(delivery_boy);
        }

        // GET: Delivery_boy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_boy delivery_boy = db.Delivery_boy.Find(id);
            if (delivery_boy == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserID", "FullName", delivery_boy.UserId);
            return View(delivery_boy);
        }

        // POST: Delivery_boy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,CNIC,Address,ContactNo")] Delivery_boy delivery_boy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(delivery_boy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserID", "FullName", delivery_boy.UserId);
            return View(delivery_boy);
        }

        // GET: Delivery_boy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Delivery_boy delivery_boy = db.Delivery_boy.Find(id);
            if (delivery_boy == null)
            {
                return HttpNotFound();
            }
            return View(delivery_boy);
        }

        // POST: Delivery_boy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Delivery_boy delivery_boy = db.Delivery_boy.Find(id);
            db.Delivery_boy.Remove(delivery_boy);
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

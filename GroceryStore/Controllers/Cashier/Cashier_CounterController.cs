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
using GroceryStore.Utilities;

namespace GroceryStore.Controllers.Cashier
{
    public class Cashier_CounterController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: Cashier_Counter
        public ActionResult Index()
        {
            string rolename = (Session["RoleName"] != null) ? Session["RoleName"].ToString() : "";
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;


            GroceryStoreEntities db = new GroceryStoreEntities();

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            if (rolename != AppRoles.Admin)
                return RedirectToAction("Auth", "Auth");

            return View(new UserDto() { User = user, Cashier_Counters = db.Cashier_Counter.ToList() });
        }

        // GET: Cashier_Counter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cashier_Counter cashier_Counter = db.Cashier_Counter.Find(id);
            if (cashier_Counter == null)
            {
                return HttpNotFound();
            }
            return View(cashier_Counter);
        }

        // GET: Cashier_Counter/Create
        public ActionResult Create()
        {
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;

            GroceryStoreEntities db = new GroceryStoreEntities();
            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();

            return View(new UserDto { User = user });
        }

        // POST: Cashier_Counter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Location,MaxOrderLimit")] Cashier_Counter cashier_Counter)
        {
            if (ModelState.IsValid)
            {
                db.Cashier_Counter.Add(cashier_Counter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cashier_Counter);
        }

        // GET: Cashier_Counter/Edit/5
        public ActionResult Edit(int? id)
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cashier_Counter cashier_Counter = db.Cashier_Counter.Find(id);
            if (cashier_Counter == null)
            {
                return HttpNotFound();
            }

            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();
            return View(new UserDto { User = user, cashier=cashier_Counter });
        }

        // POST: Cashier_Counter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Location,MaxOrderLimit")] Cashier_Counter cashier_Counter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cashier_Counter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashier_Counter);
        }

        // GET: Cashier_Counter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cashier_Counter cashier_Counter = db.Cashier_Counter.Find(id);
            if (cashier_Counter == null)
            {
                return HttpNotFound();
            }
            int? userId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;

            var user = db.Users.Where(x => x.UserID == userId).FirstOrDefault();
            return View(new UserDto { User = user, cashier = cashier_Counter });
        }

        // POST: Cashier_Counter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cashier_Counter cashier_Counter = db.Cashier_Counter.Find(id);
            db.Cashier_Counter.Remove(cashier_Counter);
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

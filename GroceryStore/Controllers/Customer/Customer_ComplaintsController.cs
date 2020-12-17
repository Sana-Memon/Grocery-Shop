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
    public class Customer_ComplaintsController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: Customer_Complaints
        public ActionResult Index()
        {
            var customer_Complaints = db.Customer_Complaints.Include(c => c.Complaint_Type).Include(c => c.Customer);
            return View(customer_Complaints.ToList());
        }

        // GET: Customer_Complaints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Complaints customer_Complaints = db.Customer_Complaints.Find(id);
            if (customer_Complaints == null)
            {
                return HttpNotFound();
            }
            return View(customer_Complaints);
        }

        // GET: Customer_Complaints/Create
        public ActionResult Create()
        {
            ViewBag.Complaint_type_Id = new SelectList(db.Complaint_Type, "Complaint_Type_Id", "Complaint_Tpe");
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_id", "Customer_id");
            return View();
        }

        // POST: Customer_Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Complaint_Id,Customer_Id,Complaint_type_Id,Complaint,Complaint_Status,date")] Customer_Complaints customer_Complaints)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Complaints.Add(customer_Complaints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Complaint_type_Id = new SelectList(db.Complaint_Type, "Complaint_Type_Id", "Complaint_Tpe", customer_Complaints.Complaint_type_Id);
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_id", "Customer_id", customer_Complaints.Customer_Id);
            return View(customer_Complaints);
        }

        // GET: Customer_Complaints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Complaints customer_Complaints = db.Customer_Complaints.Find(id);
            if (customer_Complaints == null)
            {
                return HttpNotFound();
            }
            ViewBag.Complaint_type_Id = new SelectList(db.Complaint_Type, "Complaint_Type_Id", "Complaint_Tpe", customer_Complaints.Complaint_type_Id);
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_id", "Customer_id", customer_Complaints.Customer_Id);
            return View(customer_Complaints);
        }

        // POST: Customer_Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Complaint_Id,Customer_Id,Complaint_type_Id,Complaint,Complaint_Status,date")] Customer_Complaints customer_Complaints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Complaints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Complaint_type_Id = new SelectList(db.Complaint_Type, "Complaint_Type_Id", "Complaint_Tpe", customer_Complaints.Complaint_type_Id);
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_id", "Customer_id", customer_Complaints.Customer_Id);
            return View(customer_Complaints);
        }

        // GET: Customer_Complaints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Complaints customer_Complaints = db.Customer_Complaints.Find(id);
            if (customer_Complaints == null)
            {
                return HttpNotFound();
            }
            return View(customer_Complaints);
        }

        // POST: Customer_Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Complaints customer_Complaints = db.Customer_Complaints.Find(id);
            db.Customer_Complaints.Remove(customer_Complaints);
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

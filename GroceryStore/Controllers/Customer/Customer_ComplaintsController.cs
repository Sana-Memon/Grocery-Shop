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

namespace GroceryStore.Controllers
{
    public class Customer_ComplaintsController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();

        // GET: Customer_Complaints
        public ActionResult Index()
        {
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != AppRoles.Customer)
            {
                return RedirectToAction("Auth", "Auth");
            }

            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();
            var customer = db.Customers.Where(x => x.UserID == user.UserID).FirstOrDefault();

            var customer_Complaints = db.Customer_Complaints.Include(c => c.Complaint_Type).Where(m => m.Customer_Id == customer.Customer_id) ;

            return View(new UserDto {  Customer_Complaints= customer_Complaints.ToList(), User = user });
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
            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }

            ViewBag.Complaint_type_Id = new SelectList(db.Complaint_Type, "Complaint_Type_Id", "Complaint_Tpe");
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();
            return View(new UserDto {User = user }); 
        }

        // POST: Customer_Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Complaint_type_Id, Complaint")] Customer_Complaints customer_Complaints)
        {
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != AppRoles.Customer)
            {
                return RedirectToAction("Auth", "Auth");
            }

            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();
            var customer = db.Customers.Where(x => x.UserID == user.UserID).FirstOrDefault();

            customer_Complaints.Customer_Id = customer.Customer_id;
            customer_Complaints.date = DateTime.Now;
            customer_Complaints.Complaint_Status = "PENDING";
            customer_Complaints.Complaint = Request.Form["customer_Complaint.Complaint"].ToString();
            db.Customer_Complaints.Add(customer_Complaints);
            db.SaveChanges();
            
            
            return RedirectToAction("Index");
      
        }

        // GET: Customer_Complaints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != AppRoles.Customer)
            {
                return RedirectToAction("Auth", "Auth");
            }

            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();
            var customer = db.Customers.Where(x => x.UserID == user.UserID).FirstOrDefault();

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
            
            return View(new UserDto { User = user , customer_Complaint = customer_Complaints});
        }

        // POST: Customer_Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            if (Session["RoleName"] == null || Session["RoleName"].ToString() != AppRoles.Customer)
            {
                return RedirectToAction("Auth", "Auth");
            }

            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();
            var customer = db.Customers.Where(x => x.UserID == user.UserID).FirstOrDefault();

            int complaint_id = Int32.Parse(Request.Form["customer_Complaint.Complaint_Id"]);

            var cust_instance = db.Customer_Complaints.Where(
                x => x.Customer_Id == customer.Customer_id && x.Complaint_Id == complaint_id
                ).FirstOrDefault();


            if (cust_instance.Complaint_Status.Contains("PENDING")) {
                cust_instance.Complaint = Request.Form["customer_Complaint.Complaint"].ToString();
                db.Entry(cust_instance).State = EntityState.Modified;
                db.SaveChanges();
            }
           
            return RedirectToAction("Index");
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
            int u_id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == u_id).FirstOrDefault();
            var customer = db.Customers.Where(x => x.UserID == user.UserID).FirstOrDefault();

            return View(new UserDto { User = user, customer_Complaint = customer_Complaints });
        }

        // POST: Customer_Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (Session["RoleName"] == null || Session["RoleName"].ToString() != AppRoles.Customer)
            {
                return RedirectToAction("Auth", "Auth");
            }

            Customer_Complaints customer_Complaints = db.Customer_Complaints.Find(id);

            if (customer_Complaints.Complaint_Status.Contains("PENDING")) {
                db.Customer_Complaints.Remove(customer_Complaints);
                db.SaveChanges();
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

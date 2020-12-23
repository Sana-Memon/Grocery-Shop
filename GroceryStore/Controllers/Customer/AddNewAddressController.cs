using GroceryStore.Controllers.Dto;
using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers.Customer
{
    public class AddNewAddressController : Controller
    {
        private GroceryStoreEntities db = new GroceryStoreEntities();
        // GET: AddNewAddress
        public ActionResult AddNewAddress()
        {
            ViewBag.saveresult = "";

            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();

            var city = db.Cities.ToList();

            var address = db.Addresses.Include("City").Where(x => x.CustomerID == id).ToList();  // .Include(x => x. ); //.Where()

            return View(new UserDto() { User = user, Cities = city, Address = address });  
        }

        [HttpPost]
        public ActionResult AddAddress()
        {
            string name = Request.Form["name"].ToString();
            string postCode = Request.Form["postCode"].ToString();
            int customerId = (Session["userID"] != null) ? Int32.Parse(Session["userID"].ToString()) : 0;
            string otherDetails = Request.Form["OtherDetails"].ToString();
            int? cityId = (Request.Form["CityId"] != null) ? (int?) int.Parse(Request.Form["CityId"].ToString()) : null;

            int? addressId = null;

            var result = Request.Form["addressId"].ToString();

            if (result != "")
            {
                addressId = (int?) Int32.Parse(result);
            }

            using (GroceryStoreEntities db = new GroceryStoreEntities())
            {


                /*Address address = new Address()
                {
                    Name = name,
                    Postcode = postCode,
                    CustomerID = customerId,
                    OtherDetails = otherDetails,
                    CityId = cityId
                };*/
                Address address = null;
                if (addressId != null)
                {
                    address = db.Addresses.Where(x => x.Id == addressId).FirstOrDefault();

                    address.Name = name;
                    address.Postcode = postCode;
                    address.CustomerID = customerId;
                    address.OtherDetails = otherDetails;
                    address.CityId = cityId;
                }else
                {
                    address = new Address()
                    {
                        Name = name,
                        Postcode = postCode,
                        CustomerID = customerId,
                        OtherDetails = otherDetails,
                        CityId = cityId
                    };

                    db.Addresses.Add(address);
                }

                db.SaveChanges();
                ViewBag.saveresult = "Address added successfully";
                //ModelState.Clear();


            }


            if (Session["RoleName"] == null)
            {
                return RedirectToAction("Auth", "Auth");
            }
            int id = Int32.Parse(Session["userID"].ToString());
            var user = db.Users.Where(x => x.UserID == id).FirstOrDefault();

            var city = db.Cities.ToList();

            return RedirectToAction("AddNewAddress", "AddNewAddress", new UserDto { User = user, Cities = city });
        }
    }
}
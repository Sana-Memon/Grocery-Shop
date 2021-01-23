using GroceryStore.Models;
using GroceryStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise(GroceryStore.Models.User userModel)
        {


            using (GroceryStoreEntities db = new GroceryStoreEntities())
            {
                var userDetail = db.Users.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                if (userDetail == null)
                {
                    //userModel.LoginErrorMsg = "Invalid UserName or Password";
                }

                else
                {
                    var role = db.Roles.Where(x => x.Id == userDetail.Role).FirstOrDefault();


                    Session["userID"] = userDetail.UserID;
                    if (role != null)
                    {
                        Session["RoleID"] = role.Id;
                        Session["RoleName"] = role.RoleName;
                    }

                    if (role?.RoleName == AppRoles.Customer)
                    {
                        return RedirectToAction("CustomerDashboard", "CustomerDashboard");
                    }
                    else if (role?.RoleName == AppRoles.Admin)
                    {
                        return RedirectToAction("AdminDashboard", "AdminDashboard");
                    }
                    else
                    {
                        return RedirectToAction("Home", "Index");
                    }
                }

                return RedirectToAction("Auth", "Auth", userModel); //458

            }
        }


        public ActionResult forgotPassword()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult forgotPassword(string EmailID)
        {
            string message = "";
            bool status = false;

            using (GroceryStoreEntities db = new GroceryStoreEntities())
            {
                var account = db.Users.Where(x => x.Email == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //send email for passwrd
                    string code = Guid.NewGuid().ToString();

                    SendVerificationLinkEmail(account.Email, code, "ResetPassword");
                    account.code = code;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset link sent to your account";
                }
                else
                {
                    message = "account not found";
                }
            }

            ViewBag.Message = message;
            return View();
        }


        [NonAction]
        public void SendVerificationLinkEmail(string Email, string activationCode, string Emailfor = "ResetPassword")
        {
            var verifyUrl = "/Auth/" + Emailfor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("sanaamemon567@gmail.com", "Forget your Password?");
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "67&456546$%3@%!^hgh";

            string subject = "";
            string body = "";
            if (Emailfor == "ResetPassword")
            {
                 subject = "Reset Password";

                body = "<br/><br/>We are excited to tell you that your password will be" +
                   " successfully recovered. Please click on the below link to verify your account" +
                   " <br/><br/> Please click on to the following link: <br /><br />" + link;
            }

            

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }


       

        public ActionResult ResetPassword(string id)
        {
            using (GroceryStoreEntities db = new GroceryStoreEntities())
            {
                var user = db.Users.Where(x => x.code == id).FirstOrDefault();
                if (user != null)
                {
                    ResertPasswordModel model = new ResertPasswordModel();
                    model.ResetCode = id;
                    return View(model);
                }

                else
                {
                    return HttpNotFound();

                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResertPasswordModel model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (GroceryStoreEntities db = new GroceryStoreEntities())
                {
                    var user = db.Users.Where(x => x.code == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.Password = model.NewPassword;
                        //make resetpasswordcode empty string now
                        user.code = "";
                        //to avoid validation issues, disable it
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "New password updated successfully";

                    }
                }

            }
            else
            {
                message = "Something Invalid";
            }
            ViewBag.message = message;
            return View(model);
        }

    }
}
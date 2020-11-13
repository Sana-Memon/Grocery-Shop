using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GroceryStore.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        // GET: Contact
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(string name,
                            string email,
                            string subject,
                            string message)
        {
            try
            {
                // try to find out the Error in your code

                SendEmailToAdmin("sp17bsse0071@maju.edu.pk",
                            "sanaamemon567@gmail.com",
                            "Email Recieved by " + name,
                            message,
                            "smtp.gmail.com",
                            587,
                            "Cl@ss!c232",
                            "264523452374",
                            true);




                SendEmailToCustomer("sp17bsse0071@maju.edu.pk",
                                    "sanaamemon567@gmail.com",
                                     email,
                                     "Email Sent Successfully by " + name,
                                     "Thank you for using our services, We resolve your isssues as soon as possible.",
                                     "smtp.gmail.com",
                                     587,
                                     "Cl@ss!c232",
                                     "264523452374",
                                     true);

            }
            catch (Exception ex)
            {
                string stackTrace = ex.StackTrace;
                Exception innerException = ex.InnerException;
                string Message = ex.Message;
            }

            // Write Code to save data into the DB


            return View();
        }


        public void SendEmailToAdmin(string emailFromAddress,
                                   string emailToAddress,
                                   string subject,
                                   string body,
                                   string smtpAddress,
                                   int portNumber,
                                   string password,
                                   string RequestNumber,
                                   bool enableSSL)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                string strBody = "<html><head><Title>Fresh Vegetables</Title></head><body bgcolor='#ccc'>  <h1>Email Notification from Customer</h1><br /><br />  <p>The message has been recieved by $$$emailUser$$$</p><br /><br />  <h3>Request Number: $$$RequestNumber$$$</h3><br />  <h3>Message: $$$body$$$</h3><br /></body></html>";
                strBody = strBody.Replace("$$$RequestNumber$$$", RequestNumber);
                strBody = strBody.Replace("$$$body$$$", body);
                mail.Body = strBody;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);

                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }

        public void SendEmailToCustomer(string emailFromAddress,
                                      string emailToAddress,
                                      string emailUser,
                                      string subject,
                                      string body,
                                      string smtpAddress,
                                      int portNumber,
                                      string password,
                                      string RequestNumber,
                                      bool enableSSL)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.CC.Add(emailUser);
                mail.Subject = subject;
                string strBody = "<html><head><Title>Fresh Fruits and Vegetables</Title></head><body bgcolor='#ccc'>  <h1>Email Notification from Customer</h1><br /><br />  <p>The message has been recieved by $$$emailUser$$$</p><br /><br />  <h3>Request Number: $$$RequestNumber$$$</h3><br />  <h3>Message: $$$body$$$</h3><br /></body></html>";
                strBody = strBody.Replace("$$$emailUser$$$", emailUser);
                strBody = strBody.Replace("$$$RequestNumber$$$", RequestNumber);
                strBody = strBody.Replace("$$$body$$$", body);
                mail.Body = strBody;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);

                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);


                }
            }
        }


    }
}
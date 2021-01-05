using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Services
{
    public class NotificationService
    {
        public void AddNotificationForAdmin(int? roleId, int? userId, int? productId)
        {
            try
            {
                GroceryStoreEntities db = new GroceryStoreEntities();

                var notificationResult = db.ProductNotifications.Where(x => x.ProductId == productId && x.UserId == userId && x.IsCompleted == false).FirstOrDefault();
                if (notificationResult != null) return;


                ProductNotification pn = new ProductNotification()
                {
                    ProductId = productId,
                    UserId = userId,
                    IsCompleted = false
                };

                db.ProductNotifications.Add(pn);
                db.SaveChanges();

                int pid = 0;

                pid = pn.Id; // Your Identity column ID


                product oneProduct = db.products.Where(x => x.product_id == productId).FirstOrDefault();

                Notification notification = new Notification()
                {
                    Message = string.Format("Product {0} is not available ", oneProduct?.product_name),
                    UserId = (roleId == 1) ? null : userId,
                    RoleId = roleId,
                    ProductNotificationId = pid,
                    IsRecieved = false
                };

                db.Notifications.Add(notification);

                db.SaveChanges();
            }catch(Exception e)
            {

            }
        }

        public List<Notification> GetNotification(int roleId, int? userId = null)
        {

            GroceryStoreEntities db = new GroceryStoreEntities();

            // get all notifications for admin.
            if (userId != null) {
                List<Notification> notifications = db.Notifications.Where(x => x.RoleId == roleId && x.UserId == userId && x.IsRecieved == false).ToList();
                return notifications;
            }
            else
            {
                List<Notification> notifications = db.Notifications.Where(x => x.RoleId == roleId && x.IsRecieved == false).ToList();
                return notifications;

            }

            //return 
        }

        public void NotificationRecievd(int Id)
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            Notification notification  = db.Notifications.Where(x => x.Id == Id).FirstOrDefault();
            notification.IsRecieved = true;
            db.SaveChanges();
        }

        public void AddNotificationForCustomer(int ProductId)
        {
            GroceryStoreEntities db = new GroceryStoreEntities();

            var productNotifiaciton = db.ProductNotifications.Where(x => x.ProductId == ProductId && x.IsCompleted == false).ToList();

            if ( productNotifiaciton != null )
            {
                //productNotifiaciton.IsCompleted = true;
                //db.SaveChanges();

                foreach (var pn in productNotifiaciton)
                {

                    var product = db.products.Where(x => x.product_id == ProductId).FirstOrDefault();
                    
                    // Role Id 3 means it is customer
                    Notification notification = new Notification()
                    {
                        Message = string.Format("Product {0} has been stocked ", product?.product_name),
                        UserId = pn.UserId,
                        RoleId = 3,
                        ProductNotificationId = pn.Id,
                        IsRecieved = false
                    };

                    pn.IsCompleted = true;
                    db.Notifications.Add(notification);


                }

                db.SaveChanges();
            }

        } 


    }
}
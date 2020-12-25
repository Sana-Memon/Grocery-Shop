using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GroceryStore.Utilities
{
    public static class AppRoles
    {
        public static string Customer = "Customer";
        public static string Admin = "Admin";
        public static string DeliveryBoy = "DeliveryBoy";
    }

    public static class OrderStatus {
        public static string PENDING = "PENDING"; // done - user
        public static string ACCEPTED = "ACCEPTED"; // admin 
        public static string DISPATCHED = "DISPATCHED"; // admin
        public static string DELIVERED = "DELIVERED"; // admin
        public static string CANCEL = "CANCEL"; // done - user
        public static string DELETED = "DELETED"; // done - user

    }
}
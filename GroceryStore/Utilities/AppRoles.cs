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
        public static string PENDING = "PENDING";
        public static string ACCEPTED = "ACCEPTED";
        public static string DISPATCHED = "DISPATCHED";
        public static string DELIVERED = "DELIVERED";
        public static string CANCEL = "CANCEL";
        public static string DELETED = "DELETED";

    }
}
using GroceryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroceryStore.Controllers.Dto
{
    public class CustomerComplaintDto
    {
        public int Complaint_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Complaint_type_Id { get; set; }
        public string Complaint { get; set; }
        public string Complaint_Status { get; set; }
        public Nullable<System.DateTime> date { get; set; }

        public virtual Complaint_Type Complaint_Type { get; set; }
        public int customerId { get; set; }



    }

}
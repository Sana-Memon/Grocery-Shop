//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroceryStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer_Complaints
    {
        public int Complaint_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Complaint_type_Id { get; set; }
        public string Complaint { get; set; }
        public string Complaint_Status { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual Complaint_Type Complaint_Type { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GroceryStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Counter_Records
    {
        public int CashierId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime TakeAwayTime { get; set; }
    
        public virtual Cashier_Counter Cashier_Counter { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual order order { get; set; }
    }
}
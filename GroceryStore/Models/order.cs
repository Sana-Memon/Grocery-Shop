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
    
    public partial class order
    {
        public order()
        {
            this.orderProductsPriors = new HashSet<orderProductsPrior>();
            this.OrderToDelivers = new HashSet<OrderToDeliver>();
            this.Customers = new HashSet<Customer>();
        }
    
        public int order_id { get; set; }
        public int customerr_id { get; set; }
        public string eval_set { get; set; }
        public string order_number { get; set; }
        public string order_dow { get; set; }
        public string order_hour_of_day { get; set; }
        public string days_since_prior_order { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual ICollection<orderProductsPrior> orderProductsPriors { get; set; }
        public virtual ICollection<OrderToDeliver> OrderToDelivers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}

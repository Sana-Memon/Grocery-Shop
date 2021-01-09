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
    
    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            this.orderProductsPriors = new HashSet<orderProductsPrior>();
            this.OrderToDelivers = new HashSet<OrderToDeliver>();
            this.Customers = new HashSet<Customer>();
            this.Counter_Records = new HashSet<Counter_Records>();
        }
    
        public int order_id { get; set; }
        public int customerr_id { get; set; }
        public string eval_set { get; set; }
        public string order_number { get; set; }
        public string order_dow { get; set; }
        public string order_hour_of_day { get; set; }
        public string days_since_prior_order { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string OrderStatus { get; set; }
        public Nullable<int> AddressId { get; set; }
        public Nullable<decimal> total_cost { get; set; }
        public string remarks { get; set; }
        public Nullable<int> total_quantity { get; set; }
        public string orderType { get; set; }
    
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderProductsPrior> orderProductsPriors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderToDeliver> OrderToDelivers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Counter_Records> Counter_Records { get; set; }
    }
}

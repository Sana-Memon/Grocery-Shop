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
    
    public partial class orderProductsPrior
    {
        public int order_id { get; set; }
        public int product_id { get; set; }
        public Nullable<int> quantity { get; set; }
        public string add_to_cart_order { get; set; }
        public string reordered { get; set; }
    
        public virtual order order { get; set; }
        public virtual product product { get; set; }
    }
}

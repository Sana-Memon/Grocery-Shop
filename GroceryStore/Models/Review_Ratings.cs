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
    
    public partial class Review_Ratings
    {
        public int R_Id { get; set; }
        public int product_id { get; set; }
        public int Rate { get; set; }
        public string Review { get; set; }
        public int Customer_ID { get; set; }
        public string Name { get; set; }
        public string ImageFilePath { get; set; }
    
        public virtual product product { get; set; }
    }
}

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
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.DealAndDiscounts = new HashSet<DealAndDiscount>();
            this.Lists = new HashSet<List>();
            this.orderProductsPriors = new HashSet<orderProductsPrior>();
            this.ProductNotifications = new HashSet<ProductNotification>();
            this.Review_Ratings = new HashSet<Review_Ratings>();
        }
    
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int category_id { get; set; }
        public string ImageFilePath { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public string FullDescription { get; set; }
        public Nullable<int> StockAmount { get; set; }
        public Nullable<int> SkuId { get; set; }
        public string MartLocation { get; set; }
        public string ImageFilePath2 { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealAndDiscount> DealAndDiscounts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<List> Lists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderProductsPrior> orderProductsPriors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductNotification> ProductNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review_Ratings> Review_Ratings { get; set; }
        public virtual SKU SKU { get; set; }
    }
}


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
    
public partial class DealAndDiscount
{

    public int ID { get; set; }

    public string DealName { get; set; }

    public string Description { get; set; }

    public Nullable<int> ProductID { get; set; }

    public System.DateTime createdDate { get; set; }

    public System.DateTime EndingDate { get; set; }



    public virtual product product { get; set; }

}

}

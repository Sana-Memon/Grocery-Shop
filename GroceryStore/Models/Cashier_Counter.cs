
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
    
public partial class Cashier_Counter
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Cashier_Counter()
    {

        this.Counter_Records = new HashSet<Counter_Records>();

    }


    public int Id { get; set; }

    public string Location { get; set; }

    public int MaxOrderLimit { get; set; }

    public int Current_Order_Count { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Counter_Records> Counter_Records { get; set; }

}

}


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
    
public partial class Customer
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Customer()
    {

        this.Customer_Referals = new HashSet<Customer_Referals>();

        this.Lists = new HashSet<List>();

        this.OurSatisfiedCustomers = new HashSet<OurSatisfiedCustomer>();

        this.orders = new HashSet<order>();

        this.Counter_Records = new HashSet<Counter_Records>();

        this.Customer_Complaints = new HashSet<Customer_Complaints>();

    }


    public int Customer_id { get; set; }

    public int UserID { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Customer_Referals> Customer_Referals { get; set; }

    public virtual User User { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<List> Lists { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<OurSatisfiedCustomer> OurSatisfiedCustomers { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<order> orders { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Counter_Records> Counter_Records { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Customer_Complaints> Customer_Complaints { get; set; }

}

}

﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GroceryStoreEntities : DbContext
    {
        public GroceryStoreEntities()
            : base("name=GroceryStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<about> abouts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Complaint_Type> Complaint_Type { get; set; }
        public virtual DbSet<Customer_Referals> Customer_Referals { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DealAndDiscount> DealAndDiscounts { get; set; }
        public virtual DbSet<Delivery_boy> Delivery_boy { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<orderProductsPrior> orderProductsPriors { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<OurSatisfiedCustomer> OurSatisfiedCustomers { get; set; }
        public virtual DbSet<OurTeam> OurTeams { get; set; }
        public virtual DbSet<Product_Location> Product_Location { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<Review_Ratings> Review_Ratings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SKU> SKUs { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Customer_Complaints> Customer_Complaints { get; set; }
        public virtual DbSet<orderProductsTrain> orderProductsTrains { get; set; }
        public virtual DbSet<OrderToDeliver> OrderToDelivers { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<ProductNotification> ProductNotifications { get; set; }
    }
}

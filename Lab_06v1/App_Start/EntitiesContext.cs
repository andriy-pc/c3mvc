using Lab_06v1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab_06v1.App_Start
{
   public partial class EntitiesContext : DbContext
    {
        public EntitiesContext()
            : base("name=EntitiesContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Salesman> Salesmens { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
    }
}
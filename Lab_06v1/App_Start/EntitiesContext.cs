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

        public virtual DbSet<Student> Students { get; set; }
    }
}
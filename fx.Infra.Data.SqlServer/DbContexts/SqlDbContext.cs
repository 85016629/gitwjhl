﻿namespace fx.Infra.Data.SqlServer
{
    using fx.Domain.Customer;
    using fx.Domain.OrderContext;
    using Microsoft.EntityFrameworkCore;
    
    public class SqlDbContext : DbContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=wjhl;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customer");
            modelBuilder.Entity<Order>()
                .ToTable("Order");
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
    }
}
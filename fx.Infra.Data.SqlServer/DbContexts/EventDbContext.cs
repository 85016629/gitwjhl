﻿using fx.Domain.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace fx.Infra.Data.SqlServer
{
    public class EventDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=wjhl;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DomainEvent>()
                .ToTable("DomainEvents");
        }

        public DbSet<DomainEvent> DomainEventStorage { get; set; }
    }
}

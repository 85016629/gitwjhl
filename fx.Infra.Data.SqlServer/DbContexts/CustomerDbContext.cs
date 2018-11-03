namespace fx.Domain.Customer
{
    using Microsoft.EntityFrameworkCore;
    public class CustomerDbContext : DbContext
    {
        public DbSet<CustomerDto> Customers { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=wlhl;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("Customer");
        }
    }
}

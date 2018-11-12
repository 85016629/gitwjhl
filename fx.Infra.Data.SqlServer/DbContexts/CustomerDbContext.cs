namespace fx.Infra.Data.SqlServer
{
    using fx.Domain.Customer;
    using Microsoft.EntityFrameworkCore;
    
    public class CustomerDbContext : DbContext
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
        }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}

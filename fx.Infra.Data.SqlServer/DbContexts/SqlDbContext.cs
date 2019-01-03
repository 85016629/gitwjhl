namespace fx.Infra.Data.SqlServer
{
    using fx.Domain.core;
    using fx.Domain.CustomerContext;
    using fx.Domain.OrderContext;
    using fx.Domain.ProductContext;
    using fx.Infra.Data.SqlSever;
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
            modelBuilder.ApplyConfiguration(new RoleMapper());
            modelBuilder.ApplyConfiguration(new UserMapper());
            modelBuilder.ApplyConfiguration(new UserRoleRelationMapper());
            //modelBuilder.Entity<Customer>()
            //    .ToTable("Customer");
            //modelBuilder.Entity<Order>()
            //    .ToTable("Order");
            //modelBuilder.Entity<ProductCatalog>()
            //    .ToTable("ProductCatalog");
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<BaseUser> BaseUsers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
    }
}

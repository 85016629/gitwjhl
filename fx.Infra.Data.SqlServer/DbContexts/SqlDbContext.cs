namespace fx.Infra.Data.SqlServer
{
    using fx.Domain.core;
    using fx.Domain.CustomerContext;
    using fx.Domain.OrderContext;
    using fx.Domain.ProductContext;
    using fx.Infra.Data.SqlServer.Mapper;
    using fx.Infra.Data.SqlSever;
    using Microsoft.EntityFrameworkCore;
    
    public class SqlDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=wjhl;Trusted_Connection=True;", b => b.UseRowNumberForPaging());
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleMapper());
            modelBuilder.ApplyConfiguration(new UserMapper());
            modelBuilder.ApplyConfiguration(new UserRoleRelationMapper());
            modelBuilder.ApplyConfiguration(new CustomerMapper());
            //modelBuilder.Entity<Customer>()
            //    .ToTable("Customer");
            //modelBuilder.Entity<Order>()
            //    .ToTable("Order");
            //modelBuilder.Entity<ProductCatalog>()
            //    .ToTable("ProductCatalog");
        }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BaseUser> BaseUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}

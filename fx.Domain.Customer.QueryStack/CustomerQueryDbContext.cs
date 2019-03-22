using fx.Domain.CustomerContext.QueryStack.DtoMappers;
using fx.Domain.CustomerContext.QueryStack.Models;
using Microsoft.EntityFrameworkCore;

namespace fx.Domain.CustomerContext.QueryStack
{
    public class CustomerQueryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=wjhl;Trusted_Connection=True;", b => b.UseRowNumberForPaging());
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerDtoDataMapper());
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<CustomerDto> CustomerDtos { get; set; }
    }
}

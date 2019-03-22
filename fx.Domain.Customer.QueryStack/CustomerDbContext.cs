using fx.Domain.CustomerContext.QueryStack.Models;
using Microsoft.EntityFrameworkCore;

namespace fx.Domain.CustomerContext.QueryStack
{
    public class CustomerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=wjhl;Trusted_Connection=True;", b => b.UseRowNumberForPaging());
            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<CustomerDto> CustomerDtos { get; set; }
    }
}

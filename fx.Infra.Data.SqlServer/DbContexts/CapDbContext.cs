using Microsoft.EntityFrameworkCore;

namespace fx.Infra.Data.SqlServer
{
    public class CapDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=capmsg;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DomainEvent>()
        //        .ToTable("DomainEvents");
        //}
    }
}
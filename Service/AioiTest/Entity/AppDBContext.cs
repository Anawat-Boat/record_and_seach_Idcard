using Microsoft.EntityFrameworkCore;

namespace AioiTest.Entity
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<TbCustomer> TbCustomers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCustomer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
        }
    }
}

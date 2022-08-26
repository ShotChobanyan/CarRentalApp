using Microsoft.EntityFrameworkCore;

namespace CarRentalApp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<RentHistory> RentHistory { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

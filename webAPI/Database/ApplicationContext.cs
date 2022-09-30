using Microsoft.EntityFrameworkCore;
using webAPI.Models;

namespace webAPI.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Car>? Cars { get; set; }

        public DbSet<CarBrake>? CarBrakes { get; set; }

        public DbSet<CarColor>? CarColors { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace RestApi_5._0.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }

    }
}

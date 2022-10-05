using Microsoft.EntityFrameworkCore;

namespace Employees_API_Project.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; } = null!;
    }
}

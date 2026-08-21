using employee.models;
using Microsoft.EntityFrameworkCore;

namespace employee.data
{
    public class AppDb : DbContext
    {
        
            public AppDb(DbContextOptions<AppDb> options)
                : base(options)
            {
            }

            public DbSet<Employee> Employees { get; set; }
        }
}

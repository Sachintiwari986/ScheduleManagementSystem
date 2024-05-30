using Microsoft.EntityFrameworkCore;
namespace ShiftManagementSystem.Models
{
    public class ShiftManagementCoreDbContext: DbContext
    {
        public ShiftManagementCoreDbContext(DbContextOptions<ShiftManagementCoreDbContext> options) : base(options)
        {

        }
        public DbSet<Shift> Shifts { get; set; }    
        public DbSet<Worker> Workers { get; set; }    

    }
}

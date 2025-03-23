using Microsoft.EntityFrameworkCore;
using my_test_net.Models;

namespace my_test_net.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Karyawan> Karyawan { get; set; }
    }
}

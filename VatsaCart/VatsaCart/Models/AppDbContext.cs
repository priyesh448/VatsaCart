using Microsoft.EntityFrameworkCore;

namespace VatsaCart.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Items> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Commerce.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}

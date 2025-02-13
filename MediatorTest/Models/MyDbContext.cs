using Microsoft.EntityFrameworkCore;

namespace MediatorTest.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;

    }
}
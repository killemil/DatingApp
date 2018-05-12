namespace DatingApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class DatingAppDbContext : DbContext
    {
        public DatingAppDbContext(DbContextOptions<DatingAppDbContext> options) : base(options)
        {
        }

        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }

    }
}

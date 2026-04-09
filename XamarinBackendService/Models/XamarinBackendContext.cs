using Microsoft.EntityFrameworkCore;
using XamarinBackendService.DataObjects;

namespace XamarinBackendService.Models
{
    public class XamarinBackendContext : DbContext
    {
        public XamarinBackendContext(DbContextOptions<XamarinBackendContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}


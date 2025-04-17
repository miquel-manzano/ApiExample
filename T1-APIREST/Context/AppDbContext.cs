using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using T1_APIREST.Models;

namespace T1_APIREST.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Film> Films { get; set; }
        public DbSet<Director> Directors { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>()
            .HasOne(f => f.Director)
            .WithMany(d => d.Films)
            .HasForeignKey(f => f.DirectorID);

        }

    }
}

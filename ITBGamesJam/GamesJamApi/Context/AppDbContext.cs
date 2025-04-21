using GamesJamApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamesJamApi.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la entidad Vote
            modelBuilder.Entity<Vote>(entity =>
            {
                // Relación con Game
                entity.HasOne(v => v.Game)
                      .WithMany(g => g.Votes)  // Si Game tiene una colección de Votes
                      .HasForeignKey(v => v.GameId)
                      .OnDelete(DeleteBehavior.Cascade); // O Restrict según tu necesidad

                // Relación con User
                entity.HasOne(v => v.User)
                      .WithMany(u => u.Votes)  // Si ApplicationUser tiene una colección de Votes
                      .HasForeignKey(v => v.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

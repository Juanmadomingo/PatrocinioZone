using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Models;

namespace PatrocinioZoneProyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas principales
        public DbSet<Club> Clubes { get; set; }
        public DbSet<Patrocinador> Patrocinadores { get; set; }
        public DbSet<ZonaPatrocinio> ZonasPatrocinio { get; set; }

        // public DbSet<Usuario> Usuarios { get; set; }  // solo si no es abstracta

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Club → Zonas
            modelBuilder.Entity<Club>()
                .HasMany(c => c.Zonas)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Patrocinador → ClubsPatrocinados
            modelBuilder.Entity<Patrocinador>()
                .HasMany(p => p.ClubsPatrocinados)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

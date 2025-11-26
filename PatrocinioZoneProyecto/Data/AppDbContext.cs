using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Models;

namespace PatrocinioZoneProyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Club> Clubes { get; set; }
        public DbSet<Patrocinador> Patrocinadores { get; set; }
        public DbSet<ZonaPatrocinio> ZonasPatrocinio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Club → Zonas
            modelBuilder.Entity<Club>()
                .HasMany(c => c.Zonas)
                .WithOne(z => z.Club)
                .HasForeignKey(z => z.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Patrocinador → ZonaPatrocinio
            modelBuilder.Entity<Patrocinador>()
                .HasMany(p => p.ZonasPatrocinadas)
                .WithOne(z => z.Patrocinador)
                .HasForeignKey(z => z.PatrocinadorId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
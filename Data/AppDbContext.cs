
using Microsoft.EntityFrameworkCore;
using PatrocinioZoneProyecto.Models;

namespace PatrocinioZoneProyecto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Club> Clubes { get; set; }
        public DbSet<Patrocinador> Patrocinadores { get; set; }
        public DbSet<ZonaPatrocinio> ZonasPatrocinio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ==================================
            // CLUB
            // ==================================
            modelBuilder.Entity<Club>().ToTable("Clubes");

            // ==================================
            // PATROCINADOR
            // ==================================
            modelBuilder.Entity<Patrocinador>().ToTable("Patrocinadores");

            // ==================================
            // ZONA PATROCINIO
            // ==================================
            modelBuilder.Entity<ZonaPatrocinio>().ToTable("ZonasPatrocinio");

            // Relación con Club (obligatoria)
            modelBuilder.Entity<ZonaPatrocinio>()
                .HasOne(z => z.Club)
                .WithMany(c => c.ZonasDePatrocinio)
                .HasForeignKey(z => z.ClubId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con Patrocinador (opcional)
            modelBuilder.Entity<ZonaPatrocinio>()
                .HasOne(z => z.Patrocinador)
                .WithMany()
                .HasForeignKey(z => z.PatrocinadorId)
                .OnDelete(DeleteBehavior.ClientSetNull); // evita multiple cascade paths
        }
    }
}

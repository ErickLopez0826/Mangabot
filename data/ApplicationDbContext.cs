using Microsoft.EntityFrameworkCore;
using MiMangaBot.Models;

namespace MiMangaBot.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Prestamo>()
                .ToTable("Prestamos")
                .HasKey(p => p.IdPrestamo);
        }
    }

}
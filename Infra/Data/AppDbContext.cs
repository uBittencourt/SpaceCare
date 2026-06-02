using Microsoft.EntityFrameworkCore;
using SpaceCare.Domain.Turistas;

namespace SpaceCare.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Turista> Turistas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Turista>()
                .HasIndex(t => t.PassaporteEspacial)
                .IsUnique();

            modelBuilder.Entity<Turista>()
                .HasIndex(t => t.Email)
                .IsUnique();

            modelBuilder.Entity<Turista>()
                .Property(t => t.Ativo)
                .HasColumnType("STRING(1)")
                .HasDefaultValue("1");
        }
    }
}

using API_Torneio.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_Torneio.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Lutador> Lutadores { get; set; }
        public DbSet<Torneio> Torneios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Torneio>()
                .HasMany(t => t.Participantes)
                .WithMany(l => l.Torneios)
                .UsingEntity(j => j.ToTable("TorneioLutador"));


            modelBuilder.Entity<Torneio>()
                .HasOne(t => t.Vencedor)
                .WithMany()
                .HasForeignKey(t => t.VencedorId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}


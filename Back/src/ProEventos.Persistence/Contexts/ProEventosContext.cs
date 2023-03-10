using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contexts
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {
            
        }
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Palestrante> Palestrante { get; set; }
        public DbSet<Lote> Lote { get; set; }
        public DbSet<RedeSocial> RedeSocial { get; set; }
        public DbSet<PalestranteEvento> PalestranteEvento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<PalestranteEvento>()
            .HasKey(PE => new {PE.EventoId, PE.PalestranteId});

            modelBuilder.Entity<Evento>().HasMany(e => e.Lotes).WithOne(l => l.Evento).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Evento>().HasMany(e => e.RedesSociais).WithOne(rs => rs.Evento).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Palestrante>().HasMany(p => p.RedesSociais).WithOne(rs => rs.Palestrante).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
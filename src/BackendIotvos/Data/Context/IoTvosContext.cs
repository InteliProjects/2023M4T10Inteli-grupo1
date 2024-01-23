using BackendIotvos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendIotvos.Data.Context
{
    public class IoTvosContext : DbContext
    {
        public IoTvosContext(DbContextOptions<IoTvosContext> options):
            base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Item>().Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
        }
        public DbSet<Item> Items { get; set; }

        public DbSet<Conjunto> Conjuntos { get; set; }

        public DbSet<OrdemServico> OrdensServico { get; set; }
    }
}

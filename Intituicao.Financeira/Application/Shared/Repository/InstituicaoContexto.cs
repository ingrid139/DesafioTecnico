using Intituicao.Financeira.Application.Shared.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Intituicao.Financeira.Application.Shared.Repository
{
    public class InstituicaoContexto : DbContext
    {
        public DbSet<CondicaoVeiculoDto> CondicaoVeiculos { get; set; }
        public DbSet<ContratoDto> Contratos { get; set; }
        public DbSet<PagamentoDto> Pagamentos { get; set; }
        public DbSet<StatusPagamentoDto> StatusPagamentos { get; set; }
        public DbSet<TipoVeiculoDto> TipoVeiculos { get; set; }
        public InstituicaoContexto(DbContextOptions<InstituicaoContexto> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql
                    ("Host=localhost;Port=5432;Pooling=true;Database=CatalogoDB;User Id=postgres;Password=2457;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoVeiculoDto>()
                          .HasIndex(e => e.Id)
                          .IsUnique(false);
            modelBuilder.Entity<CondicaoVeiculoDto>()
                          .HasIndex(e => e.Id)
                          .IsUnique(false);
            modelBuilder.Entity<StatusPagamentoDto>()
                           .HasIndex(e => e.Id)
                           .IsUnique(false);


            modelBuilder.Entity<TipoVeiculoDto>()
                        .HasOne(e => e.Contrato)
                        .WithOne()
                        .HasForeignKey<ContratoDto>(e => e.TipoVeiculoId);

            modelBuilder.Entity<CondicaoVeiculoDto>()
                       .HasOne(e => e.Contrato)
                       .WithOne()
                       .HasForeignKey<ContratoDto>(e => e.CondicaoVeiculoId);

            modelBuilder.Entity<StatusPagamentoDto>()
                       .HasOne(e => e.Pagamento)
                       .WithOne()
                       .HasForeignKey<PagamentoDto>(e => e.StatusPagamentoId);

            modelBuilder.Entity<ContratoDto>()
                        .HasMany(e => e.Pagamentos)
                        .WithOne()
                        .HasForeignKey(e => e.ContratoId);
        }
    }
}

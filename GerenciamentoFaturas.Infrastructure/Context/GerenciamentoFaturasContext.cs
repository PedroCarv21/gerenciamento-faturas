using GerenciamentoFaturas.Domain.Entities;
using System.Data.Common;
using System.Data.Entity;
using GerenciamentoFaturas.Infrastructure.Configurations;

namespace GerenciamentoFaturas.Infrastructure.Context
{
    public class GerenciamentoFaturasContext : DbContext
    {
        public GerenciamentoFaturasContext()
            : base("GerenciamentoFaturasConnection")
        {
        }

        public GerenciamentoFaturasContext(DbConnection connection)
            : base(connection, true)
        {
        }

        public DbSet<Fatura> Faturas { get; set; }

        public DbSet<ItemFatura> ItensFatura { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FaturaConfiguration());
            modelBuilder.Configurations.Add(new ItemFaturaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
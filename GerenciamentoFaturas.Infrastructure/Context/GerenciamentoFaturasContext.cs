using GerenciamentoFaturas.Domain.Entities;
using System.Data.Entity;

namespace GerenciamentoFaturas.Infrastructure.Context
{
    public class GerenciamentoFaturasContext : DbContext
    {
        public GerenciamentoFaturasContext()
            : base("GerenciamentoFaturasConnection")
        {
        }

        public DbSet<Fatura> Faturas { get; set; }

        public DbSet<ItemFatura> ItensFatura { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
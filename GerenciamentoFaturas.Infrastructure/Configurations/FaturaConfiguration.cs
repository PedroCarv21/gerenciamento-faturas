using GerenciamentoFaturas.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GerenciamentoFaturas.Infrastructure.Configurations
{
    public class FaturaConfiguration : EntityTypeConfiguration<Fatura>
    {
        public FaturaConfiguration()
        {
            ToTable("Faturas");

            HasKey(f => f.Id);

            Property(f => f.Numero)
                .IsRequired();

            Property(f => f.NomeCliente)
                .IsRequired()
                .HasMaxLength(100);

            Property(f => f.DataEmissao)
                .IsRequired();

            Property(f => f.Status)
                .IsRequired();

            Property(f => f.ValorTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            HasMany(f => f.Itens)
                .WithRequired(i => i.Fatura)
                .HasForeignKey(i => i.FaturaId);
        }
    }
}
using GerenciamentoFaturas.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace GerenciamentoFaturas.Infrastructure.Configurations
{
    public class ItemFaturaConfiguration : EntityTypeConfiguration<ItemFatura>
    {
        public ItemFaturaConfiguration()
        {
            ToTable("ItensFatura");

            HasKey(i => i.Id);

            Property(i => i.Descricao)
                .IsRequired()
                .HasMaxLength(150);

            Property(i => i.Quantidade)
                .IsRequired();

            Property(i => i.ValorUnitario)
                .IsRequired()
                .HasPrecision(18, 2);

            Property(i => i.ValorTotal)
                .IsRequired()
                .HasPrecision(18, 2);

            Property(i => i.Justificativa)
                .HasMaxLength(255);
        }
    }
}
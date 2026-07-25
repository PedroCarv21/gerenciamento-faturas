using System;

namespace GerenciamentoFaturas.Domain.Entities
{
    public class ItemFatura
    {
        public Guid Id { get; private set; }

        public string Descricao { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public decimal ValorTotal { get; private set; }

        public string Justificativa { get; private set; }

        public Guid FaturaId { get; private set; }

        public virtual Fatura Fatura { get; private set; }

        protected ItemFatura()
        {
        }

        public ItemFatura(
            string descricao,
            int quantidade,
            decimal valorUnitario)
        {
            Id = Guid.NewGuid();
            Descricao = descricao;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotal = quantidade * valorUnitario;
        }
    }
}

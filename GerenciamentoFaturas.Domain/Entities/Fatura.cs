using GerenciamentoFaturas.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GerenciamentoFaturas.Domain.Entities
{
    public class Fatura
    {
        public Guid Id { get; private set; }

        public int Numero { get; private set; }

        public string NomeCliente { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public StatusFatura Status { get; private set; }

        public decimal ValorTotal { get; private set; }

        public virtual ICollection<ItemFatura> Itens { get; private set; }

        protected Fatura()
        {
        }

        public Fatura(int numero, string nomeCliente, DateTime dataEmissao)
        {
            if (string.IsNullOrWhiteSpace(nomeCliente))
            {
                throw new ArgumentException("Nome do cliente obrigatório." );
            }

            Id = Guid.NewGuid();
            Numero = numero;
            NomeCliente = nomeCliente;
            DataEmissao = dataEmissao;
            Status = StatusFatura.Aberta;
            ValorTotal = 0;
            Itens = new List<ItemFatura>();
        }

        public void Atualizar(int numero, string nomeCliente, DateTime dataEmissao)
        {
            Numero = numero;
            NomeCliente = nomeCliente;
            DataEmissao = dataEmissao;
        }

        public void AdicionarItem(ItemFatura item)
        {
            item.DefinirFatura(Id);

            Itens.Add(item);

            ValorTotal += item.ValorTotal;
        }

        public void Fechar()
        {
            Status = StatusFatura.Fechada;
        }
    }
}

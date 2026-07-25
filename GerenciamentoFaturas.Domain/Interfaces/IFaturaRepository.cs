using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GerenciamentoFaturas.Domain.Interfaces
{
    public interface IFaturaRepository
    {
        void Adicionar(Fatura fatura);

        void Atualizar(Fatura fatura);

        void Fechar(Fatura fatura);

        Fatura ObterPorId(Guid id);

        IEnumerable<Fatura> Consultar(
            string nomeCliente,
            DateTime? dataEmissao,
            StatusFatura? status);

        void Salvar();
    }
}
using GerenciamentoFaturas.Domain.Entities;
using System;

namespace GerenciamentoFaturas.Domain.Interfaces
{
    public interface IItemFaturaRepository
    {
        void Adicionar(ItemFatura item);

        ItemFatura ObterPorId(Guid id);

        void Atualizar(ItemFatura item);

        void Remover(ItemFatura item);

        void Salvar();

    }
}
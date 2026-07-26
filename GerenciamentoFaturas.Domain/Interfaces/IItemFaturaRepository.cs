using GerenciamentoFaturas.Domain.Entities;

namespace GerenciamentoFaturas.Domain.Interfaces
{
    public interface IItemFaturaRepository
    {
        void Adicionar(ItemFatura item);

        void Salvar();
    }
}
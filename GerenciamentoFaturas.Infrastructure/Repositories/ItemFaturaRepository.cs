using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Interfaces;
using GerenciamentoFaturas.Infrastructure.Context;

namespace GerenciamentoFaturas.Infrastructure.Repositories
{
    public class ItemFaturaRepository : IItemFaturaRepository
    {
        private readonly GerenciamentoFaturasContext _context;

        public ItemFaturaRepository(GerenciamentoFaturasContext context)
        {
            _context = context;
        }

        public void Adicionar(ItemFatura item)
        {
            _context.ItensFatura.Add(item);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
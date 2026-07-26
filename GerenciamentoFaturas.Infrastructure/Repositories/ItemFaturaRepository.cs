using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Interfaces;
using GerenciamentoFaturas.Infrastructure.Context;
using System;

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

        public ItemFatura ObterPorId(Guid id)
        {
            return _context.ItensFatura.Find(id);
        }

        public void Atualizar(ItemFatura item)
        {
            _context.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        public void Remover(ItemFatura item)
        {
            _context.ItensFatura.Remove(item);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
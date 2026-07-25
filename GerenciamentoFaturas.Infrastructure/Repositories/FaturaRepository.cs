using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Enums;
using GerenciamentoFaturas.Domain.Interfaces;
using GerenciamentoFaturas.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GerenciamentoFaturas.Infrastructure.Repositories
{
    public class FaturaRepository : IFaturaRepository, IDisposable
    {
        private readonly GerenciamentoFaturasContext _contexto;

        public FaturaRepository(GerenciamentoFaturasContext contexto)
        {
            _contexto = contexto;
        }

        public void Adicionar(Fatura fatura)
        {
            _contexto.Faturas.Add(fatura);
        }

        public void Atualizar(Fatura fatura)
        {
            _contexto.Entry(fatura).State = EntityState.Modified;
        }

        public Fatura ObterPorId(Guid id)
        {
            return _contexto.Faturas
                .Include(f => f.Itens)
                .FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Fatura> Consultar(
            string nomeCliente,
            DateTime? dataEmissao,
            StatusFatura? status)
        {
            IQueryable<Fatura> consulta = _contexto.Faturas
                .Include(f => f.Itens);

            if (!string.IsNullOrWhiteSpace(nomeCliente))
            {
                consulta = consulta.Where(f =>
                    f.NomeCliente.Contains(nomeCliente));
            }

            if (dataEmissao.HasValue)
            {
                consulta = consulta.Where(f =>
                    DbFunctions.TruncateTime(f.DataEmissao) ==
                    DbFunctions.TruncateTime(dataEmissao.Value));
            }

            if (status.HasValue)
            {
                consulta = consulta.Where(f =>
                    f.Status == status.Value);
            }

            return consulta.ToList();
        }

        public void Salvar()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }

        public void Fechar(Fatura fatura)
        {
            _contexto.Entry(fatura).State = EntityState.Modified;
        }
    }
}
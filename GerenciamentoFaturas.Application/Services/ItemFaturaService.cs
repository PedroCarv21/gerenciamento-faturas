using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Interfaces;
using GerenciamentoFaturas.Application.Mappers;
using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Enums;
using GerenciamentoFaturas.Domain.Exceptions;
using GerenciamentoFaturas.Domain.Interfaces;
using GerenciamentoFaturas.Infrastructure.Mappers;
using System;
using System.Linq;

namespace GerenciamentoFaturas.Application.Services
{
    public class ItemFaturaService : IItemFaturaService
    {
        private readonly IItemFaturaRepository _itemFaturaRepository;
        private readonly IFaturaRepository _faturaRepository;

        public ItemFaturaService(
            IItemFaturaRepository itemFaturaRepository,
            IFaturaRepository faturaRepository)
        {
            _itemFaturaRepository = itemFaturaRepository;
            _faturaRepository = faturaRepository;
        }

        public FaturaItensResponseDto Adicionar(Guid faturaId, ItemFaturaRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var fatura = _faturaRepository.ObterPorId(faturaId);

            if (fatura == null)
            {
                throw new FaturaNaoEncontradaException();
            }

            if (fatura.Status == StatusFatura.Fechada)
            {
                throw new FaturaFechadaException();
            }

            var item = ItemFaturaMapper.ToEntity(request);

            if (item.ValorTotal > 1000 &&
                string.IsNullOrWhiteSpace(request.Justificativa))
            {
                throw new ArgumentException(
                    "É preciso informar uma justificativa se o valor total do item for maior que R$ 1000,00.");
            }

            fatura.AdicionarItem(item);

            _itemFaturaRepository.Adicionar(item);

            _faturaRepository.Atualizar(fatura);

            _itemFaturaRepository.Salvar();

            return FaturaMapper.ToFaturaItensDtoResponse(fatura);
        }

        public FaturaItensResponseDto Atualizar(Guid id, ItemFaturaRequestDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var item = _itemFaturaRepository.ObterPorId(id);

            if (item == null)
                throw new ArgumentException("Item da fatura não encontrado.");

            var fatura = _faturaRepository.ObterPorId(item.FaturaId);

            if (fatura == null)
                throw new FaturaNaoEncontradaException();

            if (fatura.Status == StatusFatura.Fechada)
                throw new FaturaFechadaException();

            item.Atualizar(
                request.Descricao,
                request.Quantidade,
                request.ValorUnitario,
                request.Justificativa);

            fatura.RecalcularValorTotal();

            _itemFaturaRepository.Atualizar(item);
            _itemFaturaRepository.Salvar();

            return FaturaMapper.ToFaturaItensDtoResponse(fatura);
        }

        public void Remover(Guid id)
        {
            var item = _itemFaturaRepository.ObterPorId(id);

            if (item == null)
                throw new ArgumentException("Item da fatura não encontrado.");

            var fatura = _faturaRepository.ObterPorId(item.FaturaId);

            if (fatura == null)
                throw new FaturaNaoEncontradaException();

            if (fatura.Status == StatusFatura.Fechada)
                throw new FaturaFechadaException();

            fatura.Itens.Remove(item);

            fatura.RecalcularValorTotal();

            _itemFaturaRepository.Remover(item);

            _itemFaturaRepository.Salvar();
        }
    }
}
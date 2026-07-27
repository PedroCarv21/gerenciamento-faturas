using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Interfaces;
using GerenciamentoFaturas.Application.Mappers;
using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Exceptions;
using GerenciamentoFaturas.Domain.Interfaces;
using GerenciamentoFaturas.Infrastructure.Mappers;
using System;

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
            Validar(request);

            var fatura = ObterFatura(faturaId);

            var item = ItemFaturaMapper.ToEntity(request);

            ValidarJustificativa(item, request);

            fatura.AdicionarItem(item);

            _itemFaturaRepository.Adicionar(item);

            _faturaRepository.Atualizar(fatura);

            _itemFaturaRepository.Salvar();

            return FaturaMapper.ToFaturaItensDtoResponse(fatura);
        }

        public FaturaItensResponseDto Atualizar(Guid faturaId, Guid id, ItemFaturaRequestDto request)
        {
            Validar(request);

            var item = ObterItem(id);

            VerificarFaturaId(faturaId, item);

            var fatura = ObterFatura(faturaId);

            item.Atualizar(
                request.Descricao,
                request.Quantidade,
                request.ValorUnitario,
                request.Justificativa);

            ValidarJustificativa(item, request);

            fatura.RecalcularValorTotal();

            _itemFaturaRepository.Atualizar(item);

            _itemFaturaRepository.Salvar();

            return FaturaMapper.ToFaturaItensDtoResponse(fatura);
        }

        public void Remover(Guid faturaId, Guid id)
        {
            var item = ObterItem(id);

            VerificarFaturaId(faturaId, item);

            var fatura = ObterFatura(faturaId);

            fatura.RemoverItem(item);

            _itemFaturaRepository.Remover(item);

            _itemFaturaRepository.Salvar();
        }

        private static void VerificarFaturaId(Guid faturaId, ItemFatura item)
        {
            if (item.FaturaId != faturaId)
            {
                throw new ArgumentException("O item informado não pertence à fatura.");
            }
        }

        private Fatura ObterFatura(Guid id)
        {
            var fatura = _faturaRepository.ObterPorId(id);

            if (fatura == null)
            {
                throw new FaturaNaoEncontradaException();
            }

            if (fatura.EstaFechada())
            {
                throw new FaturaFechadaException();
            }

            return fatura;
        }

        private ItemFatura ObterItem(Guid id)
        {
            var item = _itemFaturaRepository.ObterPorId(id);

            if (item == null)
            {
                throw new ArgumentException("Item da fatura não encontrado.");
            }

            return item;
        }

        private static void Validar(ItemFaturaRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.Descricao))
            {
                throw new ArgumentException("A descrição é obrigatória.");
            }

            if (request.Descricao.Trim().Length < 3)
            {
                throw new ArgumentException("A descrição deve ter no mínimo 3 caracteres.");
            }

            if (request.Quantidade <= 0)
            {
                throw new ArgumentException("A quantidade deve ser maior que zero.");
            }

            if (request.ValorUnitario <= 0)
            {
                throw new ArgumentException("O valor unitário deve ser maior que zero.");
            }
        }

        private static void ValidarJustificativa(ItemFatura item, ItemFaturaRequestDto request)
        {
            if (item.ValorTotal > 1000 &&
                string.IsNullOrWhiteSpace(request.Justificativa))
            {
                throw new ArgumentException(
                    "É preciso informar uma justificativa se o valor total do item for maior que R$ 1000,00.");
            }
        }
    }
}
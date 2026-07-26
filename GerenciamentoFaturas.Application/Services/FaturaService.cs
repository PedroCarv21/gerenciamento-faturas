using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Interfaces;
using GerenciamentoFaturas.Application.Mappers;
using GerenciamentoFaturas.Domain.Enums;
using GerenciamentoFaturas.Domain.Exceptions;
using GerenciamentoFaturas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoFaturas.Application.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly IFaturaRepository _faturaRepository;

        public FaturaService(IFaturaRepository faturaRepository)
        {
            _faturaRepository = faturaRepository;
        }

        public FaturaResponseDto Adicionar(FaturaRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var fatura = FaturaMapper.ToEntity(request);

            _faturaRepository.Adicionar(fatura);
            _faturaRepository.Salvar();

            return FaturaMapper.ToResponseDto(fatura);
        }

        public FaturaItensResponseDto Atualizar(Guid id, FaturaRequestDto request)
        {
            var fatura = _faturaRepository.ObterPorId(id);

            if (fatura == null)
            {
                throw new FaturaNaoEncontradaException();
            }

            if (fatura.Status == StatusFatura.Fechada)
            {
                throw new FaturaFechadaException();
            }

            fatura.Atualizar(
                request.Numero,
                request.NomeCliente,
                request.DataEmissao);

            _faturaRepository.Atualizar(fatura);
            _faturaRepository.Salvar();

            return FaturaMapper.ToFaturaItensDtoResponse(fatura);
        }

        public IEnumerable<FaturaItensResponseDto> Consultar(
            string nomeCliente,
            DateTime? dataEmissao,
            StatusFatura? status)
        {
            return _faturaRepository
                .Consultar(nomeCliente, dataEmissao, status)
                .Select(FaturaMapper.ToFaturaItensDtoResponse)
                .ToList();
        }

        public FaturaItensResponseDto Fechar(Guid id)
        {
            var fatura = _faturaRepository.ObterPorId(id);

            if (fatura == null)
            {
                throw new FaturaNaoEncontradaException();
            }

            if (fatura.Status == StatusFatura.Fechada)
            {
                throw new FaturaFechadaException();
            }

            fatura.Fechar();

            _faturaRepository.Fechar(fatura);
            _faturaRepository.Salvar();

            return FaturaMapper.ToFaturaItensDtoResponse(fatura);
        }
    }
}
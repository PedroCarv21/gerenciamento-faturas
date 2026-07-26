using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Enums;
using GerenciamentoFaturas.Infrastructure.Mappers;
using System.Linq;

namespace GerenciamentoFaturas.Application.Mappers
{
    public static class FaturaMapper
    {
        public static Fatura ToEntity(FaturaRequestDto request)
        {
            if (request == null)
            {
                return null;
            }

            return new Fatura(
                request.Numero,
                request.NomeCliente,
                request.DataEmissao);
        }
        public static FaturaResponseDto ToResponseDto(Fatura fatura)
        {
            if (fatura == null)
            {
                return null;
            }

            return new FaturaResponseDto
            {
                Id = fatura.Id,
                Numero = fatura.Numero,
                NomeCliente = fatura.NomeCliente,
                DataEmissao = fatura.DataEmissao,
                Status = fatura.Status,
                ValorTotal = fatura.ValorTotal
            };
        }

        public static FaturaItensResponseDto ToFaturaItensDtoResponse(Fatura fatura)
        {
            return new FaturaItensResponseDto
            {
                Id = fatura.Id,
                Numero = fatura.Numero,
                NomeCliente = fatura.NomeCliente,
                DataEmissao = fatura.DataEmissao,
                Status = fatura.Status,
                ValorTotal = fatura.ValorTotal,

                Itens = (fatura.Itens ?? Enumerable.Empty<ItemFatura>())
                .Select(ItemFaturaMapper.ToResponseDto)
                .ToList()
            };
        }
    }
}
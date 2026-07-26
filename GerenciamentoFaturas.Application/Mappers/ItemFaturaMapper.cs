using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Domain.Entities;

namespace GerenciamentoFaturas.Infrastructure.Mappers
{
    public static class ItemFaturaMapper
    {
        public static ItemFatura ToEntity(ItemFaturaRequestDto request)
        {
            return new ItemFatura(
                request.Descricao,
                request.Quantidade,
                request.ValorUnitario,
                request.Justificativa);
        }

        public static ItemFaturaResponseDto ToResponseDto(ItemFatura item)
        {
            return new ItemFaturaResponseDto
            {
                Id = item.Id,
                FaturaId = item.FaturaId,
                Descricao = item.Descricao,
                Quantidade = item.Quantidade,
                ValorUnitario = item.ValorUnitario,
                ValorTotal = item.ValorTotal,
                Justificativa = item.Justificativa
            };
        }
    }
}
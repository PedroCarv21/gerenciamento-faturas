using GerenciamentoFaturas.Application.DTOs;
using System;

namespace GerenciamentoFaturas.Application.Interfaces
{
    public interface IItemFaturaService
    {
        FaturaItensResponseDto Adicionar(Guid faturaId, ItemFaturaRequestDto request);
    }
}
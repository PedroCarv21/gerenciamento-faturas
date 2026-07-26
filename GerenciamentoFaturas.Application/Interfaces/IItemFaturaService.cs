using GerenciamentoFaturas.Application.DTOs;
using System;

namespace GerenciamentoFaturas.Application.Interfaces
{
    public interface IItemFaturaService
    {
        FaturaItensResponseDto Adicionar(Guid faturaId, ItemFaturaRequestDto request);

        FaturaItensResponseDto Atualizar(Guid id, ItemFaturaRequestDto request);

        void Remover(Guid id);
    }
}
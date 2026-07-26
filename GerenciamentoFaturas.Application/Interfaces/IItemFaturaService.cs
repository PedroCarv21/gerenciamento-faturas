using GerenciamentoFaturas.Application.DTOs;
using System;

namespace GerenciamentoFaturas.Application.Interfaces
{
    public interface IItemFaturaService
    {
        FaturaItensResponseDto Adicionar(Guid faturaId, ItemFaturaRequestDto request);

        FaturaItensResponseDto Atualizar(Guid faturaId, Guid id, ItemFaturaRequestDto request);

        void Remover(Guid faturaId, Guid id);
    }
}
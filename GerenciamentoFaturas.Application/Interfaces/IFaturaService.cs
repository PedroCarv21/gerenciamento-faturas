using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GerenciamentoFaturas.Application.Interfaces
{
    public interface IFaturaService
    {
        FaturaResponseDto Adicionar(FaturaRequestDto request);

        FaturaItensResponseDto Atualizar(Guid id, FaturaRequestDto request);

        IEnumerable<FaturaItensResponseDto> Consultar(
            string nomeCliente,
            DateTime? dataEmissao,
            StatusFatura? status);

        FaturaItensResponseDto Fechar(Guid id);
    }
}
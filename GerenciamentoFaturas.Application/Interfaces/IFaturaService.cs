using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GerenciamentoFaturas.Application.Interfaces
{
    public interface IFaturaService
    {
        FaturaResponseDto Adicionar(FaturaRequestDto request);

        FaturaResponseDto Atualizar(Guid id, FaturaRequestDto request);

        IEnumerable<FaturaResponseDto> Consultar(
            string nomeCliente,
            DateTime? dataEmissao,
            StatusFatura? status);

        FaturaResponseDto Fechar(Guid id);
    }
}
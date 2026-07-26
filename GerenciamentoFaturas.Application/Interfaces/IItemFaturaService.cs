using GerenciamentoFaturas.Application.DTOs;

namespace GerenciamentoFaturas.Application.Interfaces
{
    public interface IItemFaturaService
    {
        FaturaItensResponseDto Adicionar(ItemFaturaRequestDto request);
    }
}
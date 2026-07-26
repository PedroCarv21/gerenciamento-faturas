using System;

namespace GerenciamentoFaturas.Application.DTOs
{
    public class ItemFaturaRequestDto
    {
        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public string Justificativa { get; set; }
    }
}

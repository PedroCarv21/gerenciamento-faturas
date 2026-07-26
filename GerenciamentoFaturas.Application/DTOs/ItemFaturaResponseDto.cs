using System;

namespace GerenciamentoFaturas.Application.DTOs
{
    public class ItemFaturaResponseDto
    {
        public Guid Id { get; set; }

        public Guid FaturaId { get; set; }

        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public decimal ValorTotal { get; set; }

        public string Justificativa { get; set; }
    }
}

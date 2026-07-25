using GerenciamentoFaturas.Domain.Enums;
using System;

namespace GerenciamentoFaturas.Application.DTOs
{
    public class AdicionarFaturaResponseDto
    {
        public Guid Id { get; set; }

        public int Numero { get; set; }

        public string NomeCliente { get; set; }

        public DateTime DataEmissao { get; set; }

        public StatusFatura Status { get; set; }

        public decimal ValorTotal { get; set; }
    }
}
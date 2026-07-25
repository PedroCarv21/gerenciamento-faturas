using System;

namespace GerenciamentoFaturas.Application.DTOs
{
    public class AdicionarFaturaRequestDto
    {
        public int Numero { get; set; }

        public string NomeCliente { get; set; }

        public DateTime DataEmissao { get; set; }
    }
}
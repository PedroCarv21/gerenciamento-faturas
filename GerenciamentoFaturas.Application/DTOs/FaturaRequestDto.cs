using System;
using System.ComponentModel.DataAnnotations;

namespace GerenciamentoFaturas.Application.DTOs
{
    public class FaturaRequestDto
    {
        public int Numero { get; set; }

        [Required(ErrorMessage = "Nome do cliente obrigatório.")]
        [StringLength(150)]
        public string NomeCliente { get; set; }

        public DateTime DataEmissao { get; set; }
    }
}
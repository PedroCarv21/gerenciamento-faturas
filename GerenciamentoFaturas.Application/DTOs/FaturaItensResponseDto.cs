using GerenciamentoFaturas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciamentoFaturas.Application.DTOs
{
    public class FaturaItensResponseDto
    {
        public Guid Id { get; set; }

        public int Numero { get; set; }

        public string NomeCliente { get; set; }

        public DateTime DataEmissao { get; set; }

        public StatusFatura Status { get; set; }

        public decimal ValorTotal { get; set; }

        public IList<ItemFaturaResponseDto> Itens { get; set; }
    }
}

using System;

namespace GerenciamentoFaturas.Domain.Exceptions
{
    public class FaturaNaoEncontradaException : Exception
    {
        public FaturaNaoEncontradaException()
            : base("Fatura não encontrada.")
        {
        }

        public FaturaNaoEncontradaException(string mensagem)
            : base(mensagem)
        {
        }
    }
}

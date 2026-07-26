using System;

namespace GerenciamentoFaturas.Domain.Exceptions
{
    public class FaturaFechadaException : Exception
    {
        public FaturaFechadaException()
            : base("A fatura já está fechada.")
        {
        }

        public FaturaFechadaException(string mensagem)
            : base(mensagem)
        {
        }
    }
}
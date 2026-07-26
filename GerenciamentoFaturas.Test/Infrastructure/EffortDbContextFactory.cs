using Effort;
using GerenciamentoFaturas.Infrastructure.Context;

namespace GerenciamentoFaturas.Tests.Infrastructure
{
    public static class EffortDbContextFactory
    {
        public static GerenciamentoFaturasContext CriarContexto()
        {
            var connection = DbConnectionFactory.CreateTransient();

            return new GerenciamentoFaturasContext(connection);
        }
    }
}
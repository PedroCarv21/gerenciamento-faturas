using Effort;
using GerenciamentoFaturas.Infrastructure.Context;
using GerenciamentoFaturas.Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GerenciamentoFaturas.Tests.Infrastructure
{
    public abstract class TestBase
    {
        protected GerenciamentoFaturasContext Context { get; set; }

        protected FaturaRepository FaturaRepository { get; set; }

        protected ItemFaturaRepository ItemFaturaRepository { get; set; }

        [TestInitialize]
        public virtual void Inicializar()
        {
            var connection = DbConnectionFactory.CreateTransient();

            Context = new GerenciamentoFaturasContext(connection);

            FaturaRepository = new FaturaRepository(Context);
            ItemFaturaRepository = new ItemFaturaRepository(Context);
        }

        [TestCleanup]
        public void Finalizar()
        {
            Context.Dispose();
        }
    }
}
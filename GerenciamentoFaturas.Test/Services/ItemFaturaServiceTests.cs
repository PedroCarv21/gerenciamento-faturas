using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Services;
using GerenciamentoFaturas.Domain.Entities;
using GerenciamentoFaturas.Domain.Exceptions;
using GerenciamentoFaturas.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GerenciamentoFaturas.Tests.Services
{
    [TestClass]
    public class ItemFaturaServiceTests : TestBase
    {
        private ItemFaturaService _service;

        [TestInitialize]
        public override void Inicializar()
        {
            base.Inicializar();

            _service = new ItemFaturaService(
                ItemFaturaRepository,
                FaturaRepository);
        }

        [TestMethod]
        public void Adicionar_DeveAdicionarItemComSucesso()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var request = new ItemFaturaRequestDto
            {
                Descricao = "Computador",
                Quantidade = 2,
                ValorUnitario = 500m,
                Justificativa = null
            };

            var response = _service.Adicionar(fatura.Id, request);

            Assert.IsNotNull(response);

            Assert.AreEqual(1, response.Itens.Count);

            var item = response.Itens.First();

            Assert.AreEqual("Computador", item.Descricao);
            Assert.AreEqual(2, item.Quantidade);
            Assert.AreEqual(500m, item.ValorUnitario);
            Assert.AreEqual(1000m, item.ValorTotal);

            Assert.AreEqual(1000m, response.ValorTotal);
        }

        [TestMethod]
        public void Adicionar_FaturaInexistente_DeveLancarFaturaNaoEncontradaException()
        {
            var request = new ItemFaturaRequestDto
            {
                Descricao = "Computador",
                Quantidade = 2,
                ValorUnitario = 500m
            };

            try
            {
                _service.Adicionar(Guid.NewGuid(), request);

                Assert.Fail("Era esperada uma FaturaNaoEncontradaException.");
            }
            catch (FaturaNaoEncontradaException ex)
            {
                Assert.AreEqual("Fatura não encontrada.", ex.Message);
            }
        }
    }
}
using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Services;
using GerenciamentoFaturas.Domain.Enums;
using GerenciamentoFaturas.Tests.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GerenciamentoFaturas.Tests.Services
{
    [TestClass]
    public class FaturaServiceTests : TestBase
    {
        private FaturaService _service;

        [TestInitialize]
        public override void Inicializar()
        {
            base.Inicializar();

            _service = new FaturaService(FaturaRepository);
        }

        [TestMethod]
        public void Adicionar_DeveCriarFaturaComSucesso()
        {
            
            var request = new FaturaRequestDto
            {
                Numero = 1,
                NomeCliente = "Pedro Carvalho",
                DataEmissao = DateTime.Today
            };
            
            var response = _service.Adicionar(request);

            Assert.IsNotNull(response);
            Assert.AreNotEqual(Guid.Empty, response.Id);

            Assert.AreEqual(request.Numero, response.Numero);
            Assert.AreEqual(request.NomeCliente, response.NomeCliente);
            Assert.AreEqual(request.DataEmissao, response.DataEmissao);

            Assert.AreEqual(StatusFatura.Aberta, response.Status);
            Assert.AreEqual(0m, response.ValorTotal);

            var fatura = FaturaRepository.ObterPorId(response.Id);

            Assert.IsNotNull(fatura);
        }

        [TestMethod]
        public void Adicionar_NomeClienteVazio_DeveLancarArgumentException()
        {
            var request = new FaturaRequestDto
            {
                Numero = 1,
                NomeCliente = "",
                DataEmissao = DateTime.Today
            };

            try
            {
                _service.Adicionar(request);

                Assert.Fail("Era esperada uma ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Nome do cliente obrigatório.", ex.Message);
            }
        }
    }
}
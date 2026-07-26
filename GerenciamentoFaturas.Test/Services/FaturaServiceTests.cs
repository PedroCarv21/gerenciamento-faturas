using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Services;
using GerenciamentoFaturas.Domain.Enums;
using GerenciamentoFaturas.Domain.Exceptions;
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
                NomeCliente = "Pedro",
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

        [TestMethod]
        public void Atualizar_DeveAtualizarFaturaComSucesso()
        {
            var request = new FaturaRequestDto
            {
                Numero = 1,
                NomeCliente = "Pedro",
                DataEmissao = DateTime.Today
            };

            var criada = _service.Adicionar(request);

            var atualizarRequest = new FaturaRequestDto
            {
                Numero = 2,
                NomeCliente = "Maria",
                DataEmissao = DateTime.Today.AddDays(1)
            };

            var response = _service.Atualizar(criada.Id, atualizarRequest);

            Assert.IsNotNull(response);

            Assert.AreEqual(2, response.Numero);
            Assert.AreEqual("Maria", response.NomeCliente);
            Assert.AreEqual(DateTime.Today.AddDays(1), response.DataEmissao);

            var fatura = FaturaRepository.ObterPorId(criada.Id);

            Assert.IsNotNull(fatura);
            Assert.AreEqual(2, fatura.Numero);
            Assert.AreEqual("Maria", fatura.NomeCliente);
            Assert.AreEqual(DateTime.Today.AddDays(1), fatura.DataEmissao);
        }

        [TestMethod]
        public void Atualizar_FaturaInexistente_DeveLancarFaturaNaoEncontradaException()
        {
            var request = new FaturaRequestDto
            {
                Numero = 1,
                NomeCliente = "Pedro",
                DataEmissao = DateTime.Today
            };

            try
            {
                _service.Atualizar(Guid.NewGuid(), request);

                Assert.Fail("Era esperada uma FaturaNaoEncontradaException.");
            }
            catch (FaturaNaoEncontradaException ex)
            {
                Assert.AreEqual("Fatura não encontrada.", ex.Message);
            }
        }
    }
}
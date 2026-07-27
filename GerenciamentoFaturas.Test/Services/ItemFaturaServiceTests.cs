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
        public void Adicionar_DoisItens_DeveSomarCorretamenteOValorTotalDaFatura()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            _service.Adicionar(
                fatura.Id,
                new ItemFaturaRequestDto
                {
                    Descricao = "Computador",
                    Quantidade = 1,
                    ValorUnitario = 500m
                });

            var response = _service.Adicionar(
                fatura.Id,
                new ItemFaturaRequestDto
                {
                    Descricao = "Teclado",
                    Quantidade = 2,
                    ValorUnitario = 150m
                });

            Assert.AreEqual(2, response.Itens.Count);

            Assert.AreEqual(800m, response.ValorTotal);

            Assert.AreEqual(500m, response.Itens.First().ValorTotal);

            Assert.AreEqual(300m, response.Itens.Last().ValorTotal);
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

        [TestMethod]
        public void Adicionar_FaturaFechada_DeveLancarFaturaFechadaException()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            fatura.Fechar();

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var request = new ItemFaturaRequestDto
            {
                Descricao = "Computador",
                Quantidade = 2,
                ValorUnitario = 500m
            };

            try
            {
                _service.Adicionar(fatura.Id, request);

                Assert.Fail("Era esperada uma FaturaFechadaException.");
            }
            catch (FaturaFechadaException ex)
            {
                Assert.AreEqual("A fatura já está fechada.", ex.Message);
            }
        }

        [TestMethod]
        public void Adicionar_DescricaoVazia_DeveLancarArgumentException()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var request = new ItemFaturaRequestDto
            {
                Descricao = "",
                Quantidade = 1,
                ValorUnitario = 100m
            };

            try
            {
                _service.Adicionar(fatura.Id, request);

                Assert.Fail("Era esperada uma ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("A descrição é obrigatória.", ex.Message);
            }
        }

        [TestMethod]
        public void Adicionar_DescricaoMenorQueTresCaracteres_DeveLancarArgumentException()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var request = new ItemFaturaRequestDto
            {
                Descricao = "ab",
                Quantidade = 1,
                ValorUnitario = 100m
            };

            try
            {
                _service.Adicionar(fatura.Id, request);

                Assert.Fail("Era esperada uma ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("A descrição deve ter no mínimo 3 caracteres.", ex.Message);
            }
        }

        [TestMethod]
        public void Adicionar_QuantidadeIgualZero_DeveLancarArgumentException()
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
                Quantidade = 0,
                ValorUnitario = 100m
            };

            try
            {
                _service.Adicionar(fatura.Id, request);

                Assert.Fail("Era esperada uma ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("A quantidade deve ser maior que zero.", ex.Message);
            }
        }

        [TestMethod]
        public void Adicionar_ValorUnitarioIgualZero_DeveLancarArgumentException()
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
                Quantidade = 1,
                ValorUnitario = 0m
            };

            try
            {
                _service.Adicionar(fatura.Id, request);

                Assert.Fail("Era esperada uma ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("O valor unitário deve ser maior que zero.", ex.Message);
            }
        }

        [TestMethod]
        public void Adicionar_ValorAcimaDeMilSemJustificativa_DeveLancarArgumentException()
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
                ValorUnitario = 600m,
                Justificativa = ""
            };

            try
            {
                _service.Adicionar(fatura.Id, request);

                Assert.Fail("Era esperada uma ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(
                    "É preciso informar uma justificativa se o valor total do item for maior que R$ 1000,00.",
                    ex.Message);
            }
        }

        [TestMethod]
        public void Atualizar_DeveAtualizarItemComSucesso()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var adicionarRequest = new ItemFaturaRequestDto
            {
                Descricao = "Computador",
                Quantidade = 2,
                ValorUnitario = 500m
            };

            var response = _service.Adicionar(fatura.Id, adicionarRequest);

            var item = response.Itens.First();

            var atualizarRequest = new ItemFaturaRequestDto
            {
                Descricao = "Computador Dev",
                Quantidade = 3,
                ValorUnitario = 800m,
                Justificativa = "Equipamento para desenvolvimento."
            };

            var resultado = _service.Atualizar(
                fatura.Id,
                item.Id,
                atualizarRequest);

            Assert.IsNotNull(resultado);

            Assert.AreEqual(1, resultado.Itens.Count);

            var itemAtualizado = resultado.Itens.First();

            Assert.AreEqual("Computador Dev", itemAtualizado.Descricao);
            Assert.AreEqual(3, itemAtualizado.Quantidade);
            Assert.AreEqual(800m, itemAtualizado.ValorUnitario);
            Assert.AreEqual(2400m, itemAtualizado.ValorTotal);
            Assert.AreEqual("Equipamento para desenvolvimento.", itemAtualizado.Justificativa);

            Assert.AreEqual(2400m, resultado.ValorTotal);
        }

        [TestMethod]
        public void Atualizar_FaturaFechada_DeveLancarFaturaFechadaException()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var response = _service.Adicionar(fatura.Id, new ItemFaturaRequestDto
            {
                Descricao = "Computador",
                Quantidade = 1,
                ValorUnitario = 500m
            });

            var item = response.Itens.First();

            fatura.Fechar();
            FaturaRepository.Atualizar(fatura);
            FaturaRepository.Salvar();

            try
            {
                _service.Atualizar(
                    fatura.Id,
                    item.Id,
                    new ItemFaturaRequestDto
                    {
                        Descricao = "Computador Dev",
                        Quantidade = 2,
                        ValorUnitario = 1000m,
                        Justificativa = "Programação web"
                    });

                Assert.Fail("Era esperada uma FaturaFechadaException.");
            }
            catch (FaturaFechadaException ex)
            {
                Assert.AreEqual("A fatura já está fechada.", ex.Message);
            }
        }

        [TestMethod]
        public void Atualizar_ValorAcimaDeMilSemJustificativa_DeveLancarArgumentException()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var response = _service.Adicionar(fatura.Id, new ItemFaturaRequestDto
            {
                Descricao = "Computador",
                Quantidade = 1,
                ValorUnitario = 500m
            });

            var item = response.Itens.First();

            try
            {
                _service.Atualizar(
                    fatura.Id,
                    item.Id,
                    new ItemFaturaRequestDto
                    {
                        Descricao = "Computador",
                        Quantidade = 2,
                        ValorUnitario = 600m,
                        Justificativa = ""
                    });

                Assert.Fail("Era esperada uma ArgumentException.");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual(
                    "É preciso informar uma justificativa se o valor total do item for maior que R$ 1000,00.",
                    ex.Message);
            }
        }

        [TestMethod]
        public void Remover_DeveRemoverItemComSucesso()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var response = _service.Adicionar(
                fatura.Id,
                new ItemFaturaRequestDto
                {
                    Descricao = "Computador",
                    Quantidade = 2,
                    ValorUnitario = 500m
                });

            var item = response.Itens.First();

            _service.Remover(fatura.Id, item.Id);

            var faturaAtualizada = FaturaRepository.ObterPorId(fatura.Id);

            Assert.IsNotNull(faturaAtualizada);
            Assert.AreEqual(0, faturaAtualizada.Itens.Count);
            Assert.AreEqual(0m, faturaAtualizada.ValorTotal);
        }

        [TestMethod]
        public void Remover_FaturaFechada_DeveLancarFaturaFechadaException()
        {
            var fatura = new Fatura(
                1,
                "Pedro",
                DateTime.Today);

            FaturaRepository.Adicionar(fatura);
            FaturaRepository.Salvar();

            var response = _service.Adicionar(
                fatura.Id,
                new ItemFaturaRequestDto
                {
                    Descricao = "Computador",
                    Quantidade = 1,
                    ValorUnitario = 500m
                });

            var item = response.Itens.First();

            fatura.Fechar();

            FaturaRepository.Atualizar(fatura);
            FaturaRepository.Salvar();

            try
            {
                _service.Remover(
                    fatura.Id,
                    item.Id);

                Assert.Fail("Era esperada uma FaturaFechadaException.");
            }
            catch (FaturaFechadaException ex)
            {
                Assert.AreEqual("A fatura já está fechada.", ex.Message);
            }
        }
    }
}
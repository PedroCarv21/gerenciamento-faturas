using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Interfaces;
using GerenciamentoFaturas.Domain.Enums;
using GerenciamentoFaturas.Domain.Exceptions;
using System;
using System.Web.Http;

namespace GerenciamentoFaturas.API.Controllers
{
    [RoutePrefix("api/faturas")]
    public class FaturaController : ApiController
    {
        private readonly IFaturaService _faturaService;

        public FaturaController(IFaturaService faturaService)
        {
            _faturaService = faturaService;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Adicionar(FaturaRequestDto request)
        {
            try{
                var response = _faturaService.Adicionar(request);

                return Created("", response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Consultar(
            string nomeCliente = null,
            DateTime? dataEmissao = null,
            StatusFatura? status = null)
        {
            var response = _faturaService.Consultar(
                nomeCliente,
                dataEmissao,
                status);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult Atualizar(Guid id, FaturaRequestDto request)
        {
            try
            {
                var response = _faturaService.Atualizar(id, request);

                return Ok(response);
            }
            catch (FaturaNaoEncontradaException ex)
            {
                return NotFound();
            }
            catch (FaturaFechadaException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("{id:guid}/fechar")]
        public IHttpActionResult Fechar(Guid id)
        {
            try
            {
                var response = _faturaService.Fechar(id);

                return Ok(response);
            }
            catch (FaturaNaoEncontradaException)
            {
                return NotFound();
            }
            catch (FaturaFechadaException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
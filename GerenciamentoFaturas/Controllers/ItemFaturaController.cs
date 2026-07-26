using GerenciamentoFaturas.Application.DTOs;
using GerenciamentoFaturas.Application.Interfaces;
using GerenciamentoFaturas.Domain.Exceptions;
using System;
using System.Net;
using System.Web.Http;

namespace GerenciamentoFaturas.API.Controllers
{
    [RoutePrefix("api/faturas/{faturaId:guid}/itens")]
    public class ItemFaturaController : ApiController
    {
        private readonly IItemFaturaService _itemFaturaService;

        public ItemFaturaController(IItemFaturaService itemFaturaService)
        {
            _itemFaturaService = itemFaturaService;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Adicionar(Guid faturaId, ItemFaturaRequestDto request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Informe os dados do item.");
                }

                var response = _itemFaturaService.Adicionar(faturaId, request);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (FaturaNaoEncontradaException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
            catch (FaturaFechadaException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IHttpActionResult Atualizar(Guid id, ItemFaturaRequestDto request)
        {
            try
            {
                var response = _itemFaturaService.Atualizar(id, request);

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (FaturaNaoEncontradaException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
            catch (FaturaFechadaException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult Remover(Guid id)
        {
            try
            {
                _itemFaturaService.Remover(id);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (FaturaNaoEncontradaException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
            catch (FaturaFechadaException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
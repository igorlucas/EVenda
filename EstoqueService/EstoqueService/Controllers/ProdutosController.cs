using Domain;
using EstoqueService.Services;
using EstoqueService.Services.AzureServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ServiceBusMessageSender _serviceBusMessageSender;

        public ProdutosController(IProdutoService produtoService, ServiceBusMessageSender serviceBusMessageSender)
        {
            _produtoService = produtoService;
            _serviceBusMessageSender = serviceBusMessageSender;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos() => Ok(await _produtoService.ListarProdutos());

        // POST: api/Produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            try
            {
                // Adiciona produto em estoque service
                _produtoService.AdicionarProduto(produto);

                // Envia mensagem para fila do service bus
                await _serviceBusMessageSender.SendProdutoAdicionadoMessage(produto);

                // Salva alterações em estoque service
                await _produtoService.SalvarAsync();

                return StatusCode(2001, produto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Produtos
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id) return BadRequest();
            try
            {
                // Atualiza estado do produto em estoque service
                _produtoService.AtualizarProduto(produto);

                // Envia mensagem para fila do service bus
                await _serviceBusMessageSender.SendProdutoAtualizadoMessage(produto);

                // Salva alterações em estoque service
                await _produtoService.SalvarAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

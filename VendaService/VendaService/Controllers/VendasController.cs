using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendaService.Data;
using VendaService.Models;
using VendaService.Services.AzureServiceBus;

namespace VendaService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly VendaServiceContext _db;
        private readonly ServiceBusMessageSender _serviceBusMessageSender;

        public VendasController(VendaServiceContext db, ServiceBusMessageSender serviceBusMessageSender)
        {
            _db = db;
            _serviceBusMessageSender = serviceBusMessageSender;
        }

        // GET: api/vendas/produtos
        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosAVenda()
            => await _db.Produtos.Where(produto => produto.Quantidade > 0).ToListAsync();


        // POST: api/vendas
        [HttpPost("produtos/{produtoId}")]
        public async Task<ActionResult<Produto>> PostVenda(int produtoId)
        {
            try
            {
                var produto = await _db.Produtos.FindAsync(produtoId);
                if (produto == null) return NotFound();

                // Atualiza produto em vendas service
                produto.Quantidade -= 1;
                _db.Entry(produto).State = EntityState.Modified;

                // Envia mensagem para fila do service bus
                await _serviceBusMessageSender.SendProdutoVendidoMessage(produto);

                // Salva produto em venda service
                await _db.SaveChangesAsync();

                return StatusCode(201, produto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { title = "Erro ao atualizar banco de dados", message = ex.Message });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

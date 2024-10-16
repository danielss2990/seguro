using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeguroApi.Models;

namespace SeguroApi.Controllers
{
    [Route("[controller]")]
    public class VendaController : Controller
    {
        private readonly SeguroContext _context;

        public VendaController(SeguroContext context)
        {
            _context = context;
        }

        [HttpPost]

        public IActionResult AdicionaVenda([FromBody] Venda venda)
        {

            Console.WriteLine("**Venda**:", venda);
            // Verifique se a lista de itens não está vazia
            if (venda.Itens == null || venda.Itens.Count == 0)
            {
                return BadRequest("Lista de itens vazia");
            }

            // Verifique se todos os itens da lista possuem um ID de produto
            if (!venda.Itens.All(item => item.IdProduto != Guid.Empty))
            {

                return BadRequest("ID de produto não informado para algum item");
            }
            // Verifique se todos os itens da lista possuem um produto cadastrado
            var produtosIds = _context.Produtos.Select(p => p.Id).ToList();
            var itensSemProduto = venda.Itens.Where(item => !produtosIds.Contains(item.IdProduto));
            if (itensSemProduto.Any())
            {
                return BadRequest($"Produtos não cadastrados para os itens: {string.Join(", ", itensSemProduto.Select(i => i.IdProduto))}");
            }
            // Adicione a venda ao banco de dados
            _context.Vendas.Add(venda);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaVendaPorId), new { id = venda.Id }, venda);
        }

        [HttpGet]
        public IEnumerable<Venda> RecuperaVendas([FromQuery] int skip = 0,
         [FromQuery] int take = 50)
        {
            return _context.Vendas.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaVendaPorId(Guid id)
        {
            var venda = _context.Vendas.Find(id);
            if (venda == null)
            {
                return NotFound();
            }
            return Ok(venda);

        }

        [HttpPut("{id}")]
        public IActionResult AtualizaVenda(Guid id, [FromBody] Venda venda)
        {

            // Verifique se a venda existe
            var vendaExistente = _context.Vendas.Find(id);
            if (vendaExistente == null)
            {
                return NotFound();
            }

            // Verifique se a lista de itens não está vazia
            if (venda.Itens == null || venda.Itens.Count == 0)
            {
                return BadRequest("Lista de itens vazia");
            }

            // Verifique se todos os itens da lista possuem um ID de produto
            if (!venda.Itens.All(item => item.IdProduto != Guid.Empty))
            {
                return BadRequest("ID de produto não informado para algum item");
            }

            // Verifique se todos os itens da lista possuem um produto cadastrado
            var produtosIds = _context.Produtos.Select(p => p.Id).ToList();
            var itensSemProduto = venda.Itens.Where(item => !produtosIds.Contains(item.IdProduto));
            if (itensSemProduto.Any())
            {
                return BadRequest($"Produtos não cadastrados para os itens: {string.Join(", ", itensSemProduto.Select(i => i.IdProduto))}");
            }
            
            // Atualize a venda
            vendaExistente.Itens = venda.Itens;
            vendaExistente.ValorTotal = venda.ValorTotal;
            _context.SaveChanges();
            return Ok(vendaExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaVenda(Guid id)
        {
            // Verifique se a venda existe
            var vendaExistente = _context.Vendas.Find(id);
            if (vendaExistente == null)
            {
                return NotFound();
            }

            // Deleta a venda
            _context.Vendas.Remove(vendaExistente);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
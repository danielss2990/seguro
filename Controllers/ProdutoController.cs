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
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private SeguroContext _context;

        public ProdutoController(SeguroContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaProduto(
            [FromBody] Produto produto)
        {
            produto.Id = Guid.NewGuid();
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaProdutoPorId),
                new { id = produto.Id },
                produto);
        }

        [HttpGet]
        public IEnumerable<Produto> RecuperaProdutos([FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            return _context.Produtos.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaProdutoPorId(Guid id)
        {
            var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduto(Guid id, [FromBody] Produto produto)
        {
            var existingProduto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (existingProduto == null)
            {
                return NotFound();
            }

            existingProduto.Nome = produto.Nome;
            existingProduto.Valor = produto.Valor;
            existingProduto.EstoqueMinimo = produto.EstoqueMinimo;
            existingProduto.EstoqueMaximo = produto.EstoqueMaximo;
            existingProduto.SaldoEmEstoque = produto.SaldoEmEstoque;
            existingProduto.Fornecedor = produto.Fornecedor;
            existingProduto.PossuiGarantia = produto.PossuiGarantia;
            _context.SaveChanges();
            return Ok(existingProduto);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(Guid id)
        {

            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
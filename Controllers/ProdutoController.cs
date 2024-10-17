using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SeguroApi.Data.Dto;
using SeguroApi.Models;

namespace SeguroApi.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas aos produtos.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly SeguroContext _context;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="ProdutoController"/> com o contexto do banco de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public ProdutoController(SeguroContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um novo produto ao banco de dados.
        /// </summary>
        /// <param name="produtoDto">Objeto contendo os dados do produto a serem adicionados.</param>
        /// <returns>O produto criado.</returns>
        [HttpPost]
        public IActionResult AdicionaProduto([FromBody] ProdutoDTO produtoDto)
        {
            var produto = new Produto
            {
                Id = Guid.NewGuid(),
                Nome = produtoDto.Nome,
                Valor = produtoDto.Valor,
                EstoqueMinimo = produtoDto.EstoqueMinimo,
                EstoqueMaximo = produtoDto.EstoqueMaximo,
                SaldoEmEstoque = produtoDto.SaldoEmEstoque,
                Fornecedor = produtoDto.Fornecedor,
                PossuiGarantia = produtoDto.PossuiGarantia
            };

            _context.Produtos.Add(produto);
            _context.SaveChanges();
            var responseDto = new ProdutoResponseDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Valor = produto.Valor,
                EstoqueMinimo = produto.EstoqueMinimo,
                EstoqueMaximo = produto.EstoqueMaximo,
                SaldoEmEstoque = produto.SaldoEmEstoque,
                Fornecedor = produto.Fornecedor,
                PossuiGarantia = produto.PossuiGarantia
            };

            return CreatedAtAction(nameof(RecuperaProdutoPorId), new { id = produto.Id }, responseDto);
        }

        /// <summary>
        /// Recupera uma lista de produtos com suporte à paginação.
        /// </summary>
        /// <param name="skip">Número de itens a ignorar.</param>
        /// <param name="take">Número de itens a retornar.</param>
        /// <returns>Uma lista de produtos.</returns>
        [HttpGet]
        public IEnumerable<ProdutoResponseDTO> RecuperaProdutos([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _context.Produtos.Skip(skip).Take(take)
                .Select(produto => new ProdutoResponseDTO
                {
                    Id = produto.Id,
                    Nome = produto.Nome,
                    Valor = produto.Valor,
                    EstoqueMinimo = produto.EstoqueMinimo,
                    EstoqueMaximo = produto.EstoqueMaximo,
                    SaldoEmEstoque = produto.SaldoEmEstoque,
                    Fornecedor = produto.Fornecedor,
                    PossuiGarantia = produto.PossuiGarantia
                });
        }

        /// <summary>
        /// Recupera um produto específico pelo ID.
        /// </summary>
        /// <param name="id">ID do produto a ser recuperado.</param>
        /// <returns>O produto encontrado ou NotFound se não existir.</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperaProdutoPorId(Guid id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();

            var responseDto = new ProdutoResponseDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Valor = produto.Valor,
                EstoqueMinimo = produto.EstoqueMinimo,
                EstoqueMaximo = produto.EstoqueMaximo,
                SaldoEmEstoque = produto.SaldoEmEstoque,
                Fornecedor = produto.Fornecedor,
                PossuiGarantia = produto.PossuiGarantia
            };

            return Ok(responseDto);
        }

        /// <summary>
        /// Atualiza as informações de um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="produtoDto">Objeto contendo os novos dados do produto.</param>
        /// <returns>O produto atualizado ou NotFound se não for encontrado.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateProduto(Guid id, [FromBody] ProdutoDTO produtoDto)
        {
            var existingProduto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (existingProduto == null) return NotFound();

            existingProduto.Nome = produtoDto.Nome;
            existingProduto.Valor = produtoDto.Valor;
            existingProduto.EstoqueMinimo = produtoDto.EstoqueMinimo;
            existingProduto.EstoqueMaximo = produtoDto.EstoqueMaximo;
            existingProduto.SaldoEmEstoque = produtoDto.SaldoEmEstoque;
            existingProduto.Fornecedor = produtoDto.Fornecedor;
            existingProduto.PossuiGarantia = produtoDto.PossuiGarantia;
            _context.SaveChanges();

            var responseDto = new ProdutoResponseDTO
            {
                Id = existingProduto.Id,
                Nome = existingProduto.Nome,
                Valor = existingProduto.Valor,
                EstoqueMinimo = existingProduto.EstoqueMinimo,
                EstoqueMaximo = existingProduto.EstoqueMaximo,
                SaldoEmEstoque = existingProduto.SaldoEmEstoque,
                Fornecedor = existingProduto.Fornecedor,
                PossuiGarantia = existingProduto.PossuiGarantia
            };

            return Ok(responseDto);
        }

        /// <summary>
        /// Exclui um produto do banco de dados.
        /// </summary>
        /// <param name="id">ID do produto a ser excluído.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida ou NotFound se o produto não for encontrado.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduto(Guid id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null) return NotFound();

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}

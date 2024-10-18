using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SeguroApi.Data.Dto;
using SeguroApi.DTOs;
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

        /// <summary>
        /// Adiciona uma nova venda.
        /// </summary>
        /// <param name="vendaDto">Dados da venda a ser adicionada.</param>
        /// <returns>A venda criada ou erros de validação.</returns>
        [HttpPost]
        public IActionResult AdicionaVenda([FromBody] VendaDTO vendaDto)
        {
            // Verifique se a lista de itens não está vazia
            if (vendaDto.Itens == null || !vendaDto.Itens.Any())
            {
                return BadRequest("Lista de itens vazia");
            }

            // Verifique se todos os itens da lista possuem um ID de produto
            if (!vendaDto.Itens.All(item => item.IdProduto != Guid.Empty))
            {
                return BadRequest("ID de produto não informado para algum item");
            }

            // Verifique se todos os itens da lista possuem um produto cadastrado
            var produtosIds = _context.Produtos.Select(p => p.Id).ToList();
            var itensSemProduto = vendaDto.Itens.Where(item => !produtosIds.Contains(item.IdProduto));
            if (itensSemProduto.Any())
            {
                return BadRequest($"Produtos não cadastrados para os itens: {string.Join(", ", itensSemProduto.Select(i => i.IdProduto))}");
            }

            // Mapeie o VendaDTO para um objeto Venda
            var venda = new Venda
            {
                Id = Guid.NewGuid(),
                Itens = vendaDto.Itens.Select(item => new ItemVenda
                {
                    Id = Guid.NewGuid(), // Gerar um novo ID para o ItemVenda
                    IdProduto = item.IdProduto,
                    IdGarantia = item.IdGarantia,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorTotal
                }).ToList(),
                ValorTotal = vendaDto.ValorTotal
            };

        

            // Adicione a venda ao banco de dados
            _context.Vendas.Add(venda);
            _context.SaveChanges();

            // Crie um response DTO para retornar
            var responseDto = new VendaResponseDTO
            {
                Id = venda.Id,
                Itens = venda.Itens.Select(item => new ItemVendaResponseDTO
                {
                    Id = item.Id,
                    IdProduto = item.IdProduto,
                    IdGarantia = item.IdGarantia,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorTotal
                }).ToList(),
                ValorTotal = venda.ValorTotal
            };

            return CreatedAtAction(nameof(RecuperaVendaPorId), new { id = venda.Id }, responseDto);
        }

        /// <summary>
        /// Recupera uma lista de vendas paginada.
        /// </summary>
        /// <param name="skip">Número de vendas a pular.</param>
        /// <param name="take">Número máximo de vendas a retornar.</param>
        /// <returns>Lista de vendas paginada.</returns>
        [HttpGet]
        public IEnumerable<VendaResponseDTO> RecuperaVendas([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            return _context.Vendas.Skip(skip).Take(take).Select(venda => new VendaResponseDTO
            {
                Id = venda.Id,
                Itens = venda.Itens.Select(item => new ItemVendaResponseDTO
                {
                    Id = item.Id,
                    IdProduto = item.IdProduto,
                    IdGarantia = item.IdGarantia,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorTotal
                }).ToList(),
                ValorTotal = venda.ValorTotal
            });
        }

        /// <summary>
        /// Recupera uma venda específica pelo ID.
        /// </summary>
        /// <param name="id">ID da venda a ser recuperada.</param>
        /// <returns>A venda encontrada ou erro 404.</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperaVendaPorId(Guid id)
        {
            var venda = _context.Vendas.Find(id);
            if (venda == null)
            {
                return NotFound();
            }

            var responseDto = new VendaResponseDTO
            {
                Id = venda.Id,
                Itens = venda.Itens.Select(item => new ItemVendaResponseDTO
                {
                    Id = item.Id,
                    IdProduto = item.IdProduto,
                    IdGarantia = item.IdGarantia,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorTotal
                }).ToList(),
                ValorTotal = venda.ValorTotal
            };

            return Ok(responseDto);
        }

        /// <summary>
        /// Atualiza uma venda existente.
        /// </summary>
        /// <param name="id">ID da venda a ser atualizada.</param>
        /// <param name="vendaDto">Novos dados da venda.</param>
        /// <returns>A venda atualizada ou erro 404.</returns>
        [HttpPut("{id}")]
        public IActionResult AtualizaVenda(Guid id, [FromBody] VendaDTO vendaDto)
        {
            // Verifique se a venda existe
            var vendaExistente = _context.Vendas.Find(id);
            if (vendaExistente == null)
            {
                return NotFound();
            }

            // Verifique se a lista de itens não está vazia
            if (vendaDto.Itens == null || !vendaDto.Itens.Any())
            {
                return BadRequest("Lista de itens vazia");
            }

            // Verifique se todos os itens da lista possuem um ID de produto
            if (!vendaDto.Itens.All(item => item.IdProduto != Guid.Empty))
            {
                return BadRequest("ID de produto não informado para algum item");
            }

            // Verifique se todos os itens da lista possuem um produto cadastrado
            var produtosIds = _context.Produtos.Select(p => p.Id).ToList();
            var itensSemProduto = vendaDto.Itens.Where(item => !produtosIds.Contains(item.IdProduto));
            if (itensSemProduto.Any())
            {
                return BadRequest($"Produtos não cadastrados para os itens: {string.Join(", ", itensSemProduto.Select(i => i.IdProduto))}");
            }

            // Atualize a venda
            vendaExistente.Itens = vendaDto.Itens.Select(item => new ItemVenda
            {
                Id = Guid.NewGuid(), // Gerar um novo ID para o ItemVenda
                IdProduto = item.IdProduto,
                IdGarantia = item.IdGarantia,
                Quantidade = item.Quantidade,
                ValorUnitario = item.ValorUnitario,
                ValorTotal = item.ValorTotal
            }).ToList();

            vendaExistente.ValorTotal = vendaDto.ValorTotal;
            _context.SaveChanges();

            // Crie um response DTO para retornar
            var responseDto = new VendaResponseDTO
            {
                Id = vendaExistente.Id,
                Itens = vendaExistente.Itens.Select(item => new ItemVendaResponseDTO
                {
                    Id = item.Id,
                    IdProduto = item.IdProduto,
                    IdGarantia = item.IdGarantia,
                    Quantidade = item.Quantidade,
                    ValorUnitario = item.ValorUnitario,
                    ValorTotal = item.ValorTotal
                }).ToList(),
                ValorTotal = vendaExistente.ValorTotal
            };

            return Ok(responseDto);
        }

        /// <summary>
        /// Exclui uma venda pelo ID.
        /// </summary>
        /// <param name="id">ID da venda a ser excluída.</param>
        /// <returns>Status 204 ou erro 404.</returns>
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

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SeguroApi.Data.Dto;
using SeguroApi.Models;

namespace SeguroApi.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas às garantias.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GarantiaController : ControllerBase
    {
        private readonly SeguroContext _context;

        /// <summary>
        /// Inicializa uma nova instância do <see cref="GarantiaController"/>.
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        public GarantiaController(SeguroContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona uma nova garantia ao banco de dados.
        /// </summary>
        /// <param name="garantiaDTO">Objeto contendo os dados da garantia.</param>
        /// <returns>A garantia criada.</returns>
        [HttpPost]
        public IActionResult AdicionaGarantia([FromBody] GarantiaCreateDTO garantiaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var garantia = new Garantia
            {
                Id = Guid.NewGuid(),
                Nome = garantiaDTO.Nome,
                Valor = garantiaDTO.Valor,
                Prazo = garantiaDTO.Prazo
            };

            _context.Garantias.Add(garantia);
            _context.SaveChanges();

            var response = new GarantiaResponseDTO
            {
                Id = garantia.Id,
                Nome = garantia.Nome,
                Valor = garantia.Valor,
                Prazo = garantia.Prazo
            };

            return CreatedAtAction(nameof(RecuperaGarantiaPorId), new { id = garantia.Id }, response);
        }

        /// <summary>
        /// Recupera uma lista de garantias com suporte à paginação.
        /// </summary>
        /// <param name="skip">Número de itens a ignorar.</param>
        /// <param name="take">Número de itens a retornar.</param>
        /// <returns>Uma lista de garantias.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<GarantiaResponseDTO>> RecuperaGarantias([FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var garantias = _context.Garantias.Skip(skip).Take(take)
                .Select(g => new GarantiaResponseDTO
                {
                    Id = g.Id,
                    Nome = g.Nome,
                    Valor = g.Valor,
                    Prazo = g.Prazo
                }).ToList();

            return Ok(garantias);
        }

        /// <summary>
        /// Recupera uma garantia específica pelo ID.
        /// </summary>
        /// <param name="id">ID da garantia a ser recuperada.</param>
        /// <returns>A garantia encontrada ou NotFound se não existir.</returns>
        [HttpGet("{id}")]
        public IActionResult RecuperaGarantiaPorId(Guid id)
        {
            var garantia = _context.Garantias
                .Select(g => new GarantiaResponseDTO
                {
                    Id = g.Id,
                    Nome = g.Nome,
                    Valor = g.Valor,
                    Prazo = g.Prazo
                })
                .FirstOrDefault(g => g.Id == id);

            if (garantia == null)
                return NotFound();

            return Ok(garantia);
        }

        /// <summary>
        /// Atualiza as informações de uma garantia existente.
        /// </summary>
        /// <param name="id">ID da garantia a ser atualizada.</param>
        /// <param name="garantiaDTO">Objeto contendo os novos dados da garantia.</param>
        /// <returns>A garantia atualizada ou NotFound se não for encontrada.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateGarantia(Guid id, [FromBody] GarantiaUpdateDTO garantiaDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingGarantia = _context.Garantias.FirstOrDefault(g => g.Id == id);
            if (existingGarantia == null)
                return NotFound();

            existingGarantia.Nome = garantiaDTO.Nome;
            existingGarantia.Valor = garantiaDTO.Valor;
            existingGarantia.Prazo = garantiaDTO.Prazo;

            _context.SaveChanges();

            var response = new GarantiaResponseDTO
            {
                Id = existingGarantia.Id,
                Nome = existingGarantia.Nome,
                Valor = existingGarantia.Valor,
                Prazo = existingGarantia.Prazo
            };

            return Ok(response);
        }

        /// <summary>
        /// Exclui uma garantia do banco de dados.
        /// </summary>
        /// <param name="id">ID da garantia a ser excluída.</param>
        /// <returns>NoContent se a exclusão for bem-sucedida ou NotFound se a garantia não for encontrada.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteGarantia(Guid id)
        {
            var garantia = _context.Garantias.FirstOrDefault(g => g.Id == id);
            if (garantia == null)
                return NotFound();

            _context.Garantias.Remove(garantia);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

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
    public class GarantiaController : ControllerBase
    {
        private SeguroContext _context;

        public GarantiaController(SeguroContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AdicionaGarantia(
             [FromBody] Garantia garantia)
        {
            garantia.Id = Guid.NewGuid();
            _context.Garantias.Add(garantia);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaGarantiaPorId),
                new { id = garantia.Id },
                garantia);
        }

        [HttpGet]
        public IEnumerable<Garantia> RecuperaGarantias([FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            return _context.Garantias.Skip(skip).Take(take);
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaGarantiaPorId(Guid id)
        {
            var garantia = _context.Garantias.FirstOrDefault(garantia => garantia.Id == id);
            if (garantia == null) return NotFound();
            return Ok(garantia);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGarantia(Guid id, [FromBody] Garantia garantia)
        {
            var existingGarantia = _context.Garantias.FirstOrDefault(g => g.Id == id);
            if (existingGarantia == null)
            {
                return NotFound();
            }

            existingGarantia.Nome = garantia.Nome;
            existingGarantia.Valor = garantia.Valor;
            existingGarantia.Prazo = garantia.Prazo;
            _context.SaveChanges();
            return Ok(existingGarantia);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGarantia(Guid id)
        {

            var garantia = _context.Garantias.FirstOrDefault(g => g.Id == id);
            if (garantia == null)
            {
                return NotFound();
            }
            _context.Garantias.Remove(garantia);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Normas.Api.Data;
using Normas.Api.Models;

namespace Normas.Api.Controllers
{
    [Route("requisitos")]
    [ApiController]
    public class TipoRequisitoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TipoRequisitoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoRequisito
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoRequisito>>> GetTipoRequisitos()
        {
            return await _context.TipoRequisitos.ToListAsync();
        }

        // GET: api/TipoRequisito/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoRequisito>> GetTipoRequisito(int id)
        {
            var tipoRequisito = await _context.TipoRequisitos.FindAsync(id);

            if (tipoRequisito == null)
            {
                return NotFound();
            }

            return tipoRequisito;
        }

        // PUT: api/TipoRequisito/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoRequisito(int id, TipoRequisito tipoRequisito)
        {
            if (id != tipoRequisito.TipoRequisitoId)
            {
                return BadRequest();
            }

            _context.Entry(tipoRequisito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoRequisitoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TipoRequisito
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoRequisito>> PostTipoRequisito(TipoRequisito tipoRequisito)
        {
            _context.TipoRequisitos.Add(tipoRequisito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoRequisito", new { id = tipoRequisito.TipoRequisitoId }, tipoRequisito);
        }

        // DELETE: api/TipoRequisito/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoRequisito(int id)
        {
            var tipoRequisito = await _context.TipoRequisitos.FindAsync(id);
            if (tipoRequisito == null)
            {
                return NotFound();
            }

            _context.TipoRequisitos.Remove(tipoRequisito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoRequisitoExists(int id)
        {
            return _context.TipoRequisitos.Any(e => e.TipoRequisitoId == id);
        }
    }
}

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
    [Route("normas")]
    [ApiController]
    public class NormasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NormasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Normas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Norma>>> GetNormas()
        {
            var normas = _context.Normas
                 .Include(x => x.Requisito)
                 .AsNoTracking();
            
            return await normas.ToListAsync();
        }

      

        // GET: api/Normas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Norma>> GetNorma(int? id)
        {
            if (id == null) return NotFound();

            //var norma = await _context.Normas.FindAsync(id);

            var norma = await _context.Normas
                .Include(r => r.Requisito)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.NormaID == id);
            ;

            if (norma == null) return NotFound();
           
            return norma;
        }

        // PUT: api/Normas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNorma(int id, Norma norma)
        {
            if (id != norma.NormaID)
            {
                return BadRequest();
            }

            _context.Entry(norma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NormaExists(id))
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

        // POST: api/Normas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Norma>> PostNorma(Norma norma)
        {
            _context.Normas.Add(norma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNorma", new { id = norma.NormaID }, norma);
        }

        // DELETE: api/Normas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNorma(int id)
        {
            var norma = await _context.Normas.FindAsync(id);
            if (norma == null)
            {
                return NotFound();
            }

            _context.Normas.Remove(norma);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NormaExists(int id)
        {
            return _context.Normas.Any(e => e.NormaID == id);
        }
    }
}

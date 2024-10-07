using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDesafio3.Model;

namespace ProyectoDesafio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasoPreparacionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PasoPreparacionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PasoPreparacions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasoPreparacion>>> GetPasoPreparacions()
        {
            return await _context.PasoPreparacions.ToListAsync();
        }

        // GET: api/PasoPreparacions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PasoPreparacion>> GetPasoPreparacion(int id)
        {
            var pasoPreparacion = await _context.PasoPreparacions.FindAsync(id);

            if (pasoPreparacion == null)
            {
                return NotFound();
            }

            return pasoPreparacion;
        }

        // PUT: api/PasoPreparacions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPasoPreparacion(int id, PasoPreparacion pasoPreparacion)
        {
            if (id != pasoPreparacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(pasoPreparacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PasoPreparacionExists(id))
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

        // POST: api/PasoPreparacions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PasoPreparacion>> PostPasoPreparacion(PasoPreparacion pasoPreparacion)
        {
            _context.PasoPreparacions.Add(pasoPreparacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasoPreparacion", new { id = pasoPreparacion.Id }, pasoPreparacion);
        }

        // DELETE: api/PasoPreparacions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePasoPreparacion(int id)
        {
            var pasoPreparacion = await _context.PasoPreparacions.FindAsync(id);
            if (pasoPreparacion == null)
            {
                return NotFound();
            }

            _context.PasoPreparacions.Remove(pasoPreparacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PasoPreparacionExists(int id)
        {
            return _context.PasoPreparacions.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDesafio3.Model;

namespace ProyectoDesafio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecetasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RecetasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Recetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recetas>>> GetRecetas()
        {
            return await _context.Recetas.ToListAsync();
        }

        // GET: api/Recetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recetas>> GetRecetas(int id)
        {
            var recetas = await _context.Recetas.FindAsync(id);

            if (recetas == null)
            {
                return NotFound();
            }

            return recetas;
        }

        // PUT: api/Recetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecetas(int id, Recetas recetas)
        {
            if (id != recetas.Id)
            {
                return BadRequest();
            }

            _context.Entry(recetas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetasExists(id))
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

        // POST: api/Recetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recetas>> PostRecetas(Recetas recetas)
        {
            _context.Recetas.Add(recetas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecetas", new { id = recetas.Id }, recetas);
        }

        // DELETE: api/Recetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecetas(int id)
        {
            var recetas = await _context.Recetas.FindAsync(id);
            if (recetas == null)
            {
                return NotFound();
            }

            _context.Recetas.Remove(recetas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetasExists(int id)
        {
            return _context.Recetas.Any(e => e.Id == id);
        }
    }
}

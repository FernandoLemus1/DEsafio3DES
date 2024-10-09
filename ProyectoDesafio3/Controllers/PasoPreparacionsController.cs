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
    [Authorize] // Asegura que solo los usuarios autenticados puedan acceder a este controlador
    public class PasoPreparacionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PasoPreparacionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PasoPreparacions
        // Todos los usuarios autenticados pueden ver los pasos de preparación
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasoPreparacion>>> GetPasoPreparacions()
        {
            return await _context.PasoPreparacions.ToListAsync();
        }

        // GET: api/PasoPreparacions/5
        // Todos los usuarios autenticados pueden ver detalles de un paso de preparación específico
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
        // Solo los usuarios con el rol de "Administrador" pueden modificar un paso de preparación
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")] // Solo los usuarios con el rol "Administrador" pueden modificar
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
        // Solo los usuarios con el rol de "Administrador" pueden crear un nuevo paso de preparación
        [HttpPost]
        [Authorize(Roles = "Administrador")] // Solo los usuarios con el rol "Administrador" pueden crear
        public async Task<ActionResult<PasoPreparacion>> PostPasoPreparacion(PasoPreparacion pasoPreparacion)
        {
            _context.PasoPreparacions.Add(pasoPreparacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPasoPreparacion", new { id = pasoPreparacion.Id }, pasoPreparacion);
        }

        // DELETE: api/PasoPreparacions/5
        // Solo los usuarios con el rol de "Administrador" pueden eliminar un paso de preparación
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")] // Solo los usuarios con el rol "Administrador" pueden eliminar
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

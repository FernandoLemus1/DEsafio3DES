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
    [Authorize] // Esto asegura que solo los usuarios autenticados pueden acceder a este controlador.
    public class IngredientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IngredientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingredientes
        // Todos los usuarios autenticados pueden ver los ingredientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingrediente>>> GetIngredientes()
        {
            return await _context.Ingredientes.ToListAsync();
        }

        // GET: api/Ingredientes/5
        // Todos los usuarios autenticados pueden ver detalles de un ingrediente específico
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingrediente>> GetIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);

            if (ingrediente == null)
            {
                return NotFound();
            }

            return ingrediente;
        }

        // PUT: api/Ingredientes/5
        // Solo los usuarios con el rol de "Administrador" pueden modificar un ingrediente
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")] // Solo los usuarios con el rol "Administrador" pueden modificar
        public async Task<IActionResult> PutIngrediente(int id, Ingrediente ingrediente)
        {
            if (id != ingrediente.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingrediente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(id))
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

        // POST: api/Ingredientes
        // Solo los usuarios con el rol de "Administrador" pueden crear un nuevo ingrediente
        [HttpPost]
        [Authorize(Roles = "Administrador")] // Solo los usuarios con el rol "Administrador" pueden crear
        public async Task<ActionResult<Ingrediente>> PostIngrediente(Ingrediente ingrediente)
        {
            _context.Ingredientes.Add(ingrediente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngrediente", new { id = ingrediente.Id }, ingrediente);
        }

        // DELETE: api/Ingredientes/5
        // Solo los usuarios con el rol de "Administrador" pueden eliminar un ingrediente
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")] // Solo los usuarios con el rol "Administrador" pueden eliminar
        public async Task<IActionResult> DeleteIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredienteExists(int id)
        {
            return _context.Ingredientes.Any(e => e.Id == id);
        }
    }

}

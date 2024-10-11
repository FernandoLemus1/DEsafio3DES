using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoDesafio3.Model;

namespace ProyectoDesafio3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Crear el usuario
            var user = new Usuario
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Verificar si el rol existe, si no lo crea
                var roleExists = await _roleManager.RoleExistsAsync(model.Role);
                if (!roleExists)
                {
                    return BadRequest($"El rol {model.Role} no existe.");
                }

                // Asignar el rol al usuario
                await _userManager.AddToRoleAsync(user, model.Role);

                return Ok("Usuario registrado con éxito y rol asignado.");
            }

            return BadRequest(result.Errors);
        }
    }

}

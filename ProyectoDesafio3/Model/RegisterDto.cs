using System.ComponentModel.DataAnnotations;

namespace ProyectoDesafio3.Model
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}

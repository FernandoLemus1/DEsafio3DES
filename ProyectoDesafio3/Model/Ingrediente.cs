using System.ComponentModel.DataAnnotations;

namespace ProyectoDesafio3.Model
{
    public class Ingrediente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        [Required]
        public double Cantidad { get; set; }

        [Required]
        public string UnidadMedida { get; set; } 
        public int RecetaId { get; set; }
        public Recetas? Receta { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProyectoDesafio3.Model
{
    public class PasoPreparacion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 10)]
        public string Descripcion { get; set; }

        [Required]
        public int Orden { get; set; }

        public int RecetaId { get; set; }
        public Recetas? Receta { get; set; }
    }
}

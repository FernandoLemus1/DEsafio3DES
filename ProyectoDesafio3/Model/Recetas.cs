using System.ComponentModel.DataAnnotations;

namespace ProyectoDesafio3.Model
{
    public class Recetas
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nombre { get; set; }

        public string? Descripcion { get; set; }

        public int TiempoEstimadoPreparacion { get; set; }
        public ICollection<Ingrediente>? Ingredientes { get; set; }
        public ICollection<PasoPreparacion>? PasosPreparacion { get; set; }

    }
}

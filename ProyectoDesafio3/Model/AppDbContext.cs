using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoDesafio3.Model
{
    public class AppDbContext:IdentityDbContext<Usuario>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Recetas> Recetas { get; set; }
        public DbSet<PasoPreparacion> PasoPreparacions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Semilla de datos para Recetas
            modelBuilder.Entity<Recetas>().HasData(
                new Recetas { Id = 1, Nombre = "Ensalada César", Descripcion = "Ensalada clásica con pollo, lechuga y aderezo César.", TiempoEstimadoPreparacion = 20 },
                new Recetas { Id = 2, Nombre = "Pasta Carbonara", Descripcion = "Pasta con salsa de crema, huevo y queso parmesano.", TiempoEstimadoPreparacion = 30 },
                new Recetas { Id = 3, Nombre = "Sopa de Tomate", Descripcion = "Sopa ligera de tomate con albahaca.", TiempoEstimadoPreparacion = 40 }
            );

            // Semilla de datos para Ingredientes
            modelBuilder.Entity<Ingrediente>().HasData(
                // Ingredientes para Ensalada César
                new Ingrediente { Id = 1, Nombre = "Lechuga romana", Cantidad = 1, UnidadMedida = "Unidad", RecetaId = 1 },
                new Ingrediente { Id = 2, Nombre = "Pollo a la parrilla", Cantidad = 200, UnidadMedida = "Gramos", RecetaId = 1 },
                new Ingrediente { Id = 3, Nombre = "Aderezo César", Cantidad = 50, UnidadMedida = "Mililitros", RecetaId = 1 },

                // Ingredientes para Pasta Carbonara
                new Ingrediente { Id = 4, Nombre = "Pasta espagueti", Cantidad = 250, UnidadMedida = "Gramos", RecetaId = 2 },
                new Ingrediente { Id = 5, Nombre = "Crema de leche", Cantidad = 100, UnidadMedida = "Mililitros", RecetaId = 2 },
                new Ingrediente { Id = 6, Nombre = "Huevo", Cantidad = 1, UnidadMedida = "Unidad", RecetaId = 2 },
                new Ingrediente { Id = 7, Nombre = "Queso parmesano", Cantidad = 50, UnidadMedida = "Gramos", RecetaId = 2 },

                // Ingredientes para Sopa de Tomate
                new Ingrediente { Id = 8, Nombre = "Tomates frescos", Cantidad = 500, UnidadMedida = "Gramos", RecetaId = 3 },
                new Ingrediente { Id = 9, Nombre = "Albahaca", Cantidad = 5, UnidadMedida = "Hojas", RecetaId = 3 }
            );

            // Semilla de datos para Pasos de Preparación
            modelBuilder.Entity<PasoPreparacion>().HasData(
                // Pasos de preparación para Ensalada César
                new PasoPreparacion { Id = 1, Descripcion = "Lavar la lechuga romana y cortarla en trozos.", Orden = 1, RecetaId = 1 },
                new PasoPreparacion { Id = 2, Descripcion = "Asar el pollo a la parrilla y cortarlo en tiras.", Orden = 2, RecetaId = 1 },
                new PasoPreparacion { Id = 3, Descripcion = "Mezclar la lechuga, el pollo y el aderezo césar.", Orden = 3, RecetaId = 1 },

                // Pasos de preparación para Pasta Carbonara
                new PasoPreparacion { Id = 4, Descripcion = "Cocinar la pasta en agua hirviendo con sal.", Orden = 1, RecetaId = 2 },
                new PasoPreparacion { Id = 5, Descripcion = "Mezclar el huevo, la crema y el queso parmesano.", Orden = 2, RecetaId = 2 },
                new PasoPreparacion { Id = 6, Descripcion = "Añadir la mezcla a la pasta caliente.", Orden = 3, RecetaId = 2 },

                // Pasos de preparación para Sopa de Tomate
                new PasoPreparacion { Id = 7, Descripcion = "Cortar los tomates y hervirlos hasta que se ablanden.", Orden = 1, RecetaId = 3 },
                new PasoPreparacion { Id = 8, Descripcion = "Licuar los tomates y agregar la albahaca.", Orden = 2, RecetaId = 3 },
                new PasoPreparacion { Id = 9, Descripcion = "Cocinar por 10 minutos más y servir caliente.", Orden = 3, RecetaId = 3 }
            );
        }
    }
}

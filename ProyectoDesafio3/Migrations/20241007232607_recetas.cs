using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProyectoDesafio3.Migrations
{
    /// <inheritdoc />
    public partial class recetas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiempoEstimadoPreparacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredientes_Recetas_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasoPreparacions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    RecetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasoPreparacions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PasoPreparacions_Recetas_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recetas",
                columns: new[] { "Id", "Descripcion", "Nombre", "TiempoEstimadoPreparacion" },
                values: new object[,]
                {
                    { 1, "Ensalada clásica con pollo, lechuga y aderezo César.", "Ensalada César", 20 },
                    { 2, "Pasta con salsa de crema, huevo y queso parmesano.", "Pasta Carbonara", 30 },
                    { 3, "Sopa ligera de tomate con albahaca.", "Sopa de Tomate", 40 }
                });

            migrationBuilder.InsertData(
                table: "Ingredientes",
                columns: new[] { "Id", "Cantidad", "Nombre", "RecetaId", "UnidadMedida" },
                values: new object[,]
                {
                    { 1, 1.0, "Lechuga romana", 1, "Unidad" },
                    { 2, 200.0, "Pollo a la parrilla", 1, "Gramos" },
                    { 3, 50.0, "Aderezo César", 1, "Mililitros" },
                    { 4, 250.0, "Pasta espagueti", 2, "Gramos" },
                    { 5, 100.0, "Crema de leche", 2, "Mililitros" },
                    { 6, 1.0, "Huevo", 2, "Unidad" },
                    { 7, 50.0, "Queso parmesano", 2, "Gramos" },
                    { 8, 500.0, "Tomates frescos", 3, "Gramos" },
                    { 9, 5.0, "Albahaca", 3, "Hojas" }
                });

            migrationBuilder.InsertData(
                table: "PasoPreparacions",
                columns: new[] { "Id", "Descripcion", "Orden", "RecetaId" },
                values: new object[,]
                {
                    { 1, "Lavar la lechuga romana y cortarla en trozos.", 1, 1 },
                    { 2, "Asar el pollo a la parrilla y cortarlo en tiras.", 2, 1 },
                    { 3, "Mezclar la lechuga, el pollo y el aderezo césar.", 3, 1 },
                    { 4, "Cocinar la pasta en agua hirviendo con sal.", 1, 2 },
                    { 5, "Mezclar el huevo, la crema y el queso parmesano.", 2, 2 },
                    { 6, "Añadir la mezcla a la pasta caliente.", 3, 2 },
                    { 7, "Cortar los tomates y hervirlos hasta que se ablanden.", 1, 3 },
                    { 8, "Licuar los tomates y agregar la albahaca.", 2, 3 },
                    { 9, "Cocinar por 10 minutos más y servir caliente.", 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_RecetaId",
                table: "Ingredientes",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_PasoPreparacions_RecetaId",
                table: "PasoPreparacions",
                column: "RecetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "PasoPreparacions");

            migrationBuilder.DropTable(
                name: "Recetas");
        }
    }
}

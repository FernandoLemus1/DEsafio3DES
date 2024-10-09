using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoDesafio3.Model;

namespace ProyectoDesafio3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Agregar DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

            // Configuración de Identity con soporte para roles
            builder.Services.AddIdentityCore<Usuario>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<IdentityRole>() // Añadir roles a la configuración de Identity
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();

            // Agregar autenticación y autorización
            builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
                .AddCookie(IdentityConstants.ApplicationScheme);

            builder.Services.AddAuthorization(options =>
            {
                // Configurar políticas para roles
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Administrador"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("Usuario"));
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            // Autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Mapear API de Identity para usuarios
            app.MapIdentityApi<Usuario>();

            // Mapear controladores
            app.MapControllers();

            // Crear roles y usuarios iniciales al inicio
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<Usuario>>();

                // Llamar método para crear roles
                CrearRolesAsync(roleManager, userManager).Wait();
            }

            app.Run();
        }

        // Método para crear roles y usuarios predeterminados
        private static async Task CrearRolesAsync(RoleManager<IdentityRole> roleManager, UserManager<Usuario> userManager)
        {
            string[] roles = { "Administrador", "Usuario" };

            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    // Crear el rol si no existe
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Crear un usuario administrador por defecto
            var adminEmail = "admin@empresa.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var admin = new Usuario
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                var result = await userManager.CreateAsync(admin, "AdminPassword123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Administrador");
                }
            }
            // Crear un usuario normal por defecto
            var userEmail = "usuario@empresa.com";
            var normalUser = await userManager.FindByEmailAsync(userEmail);
            if (normalUser == null)
            {
                var user = new Usuario
                {
                    UserName = userEmail,
                    Email = userEmail
                };
                var result = await userManager.CreateAsync(user, "UserPassword123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Usuario");
                }
            }
        }

    }

}

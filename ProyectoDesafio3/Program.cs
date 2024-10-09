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
            builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication()
                .AddCookie(IdentityConstants.ApplicationScheme);

            builder.Services.AddIdentityCore<Usuario>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddApiEndpoints();

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

            app.UseAuthorization();
            app.UseAuthentication();
            app.MapIdentityApi<Usuario>();
            app.MapControllers();

            app.Run();
        }
    }
}

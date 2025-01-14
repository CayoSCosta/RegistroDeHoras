using Microsoft.EntityFrameworkCore;

namespace RegistroDeHoras.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Registro de horas API",
                Version = "v1",
                Description = "Gerenciamento de horas e atividades.",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Cayo Costa",
                    Email = "cayo.costa@outlook.com"
                }
            });
        });

        // Configure SQLite DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Add services to the container.

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Api.Services;

namespace RegistroDeHoras.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adicionar servi�os ao cont�iner
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configurar DbContext
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();

        builder.Services.AddScoped<ITarefaServices, TarefaServices>();
        builder.Services.AddAutoMapper(typeof(Program));

        // Adicionar CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("RegistroDeHoras.Client", policy =>
            {
                //policy.WithOrigins("https://localhost:7224")
                policy.WithOrigins("http://localhost:5200")
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API .NET 9 - v1");
                options.RoutePrefix = "swagger"; // Acessa o Swagger na URL raiz
            });
        }

        // Configurar o pipeline de solicitação HTTP
        app.UseCors("RegistroDeHoras.Client");
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}




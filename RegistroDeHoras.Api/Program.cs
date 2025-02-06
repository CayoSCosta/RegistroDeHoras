using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Api.Services;

namespace RegistroDeHoras.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        // builder.Services.AddSwaggerGen(c =>
        // {
        //     c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        //     {
        //         Title = "Registro de horas API",
        //         Version = "v1",
        //         Description = "Gerenciamento de horas e atividades.",
        //     });
        // });

        // Configure SQLite DbContext
        //builder.Services.AddDbContext<AppDbContext>(options =>
        //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var conn = builder.Configuration.GetConnectionString("DefaultConnection");


        builder.Services.AddControllers();

        builder.Services.AddScoped<ITarefaServices, TarefaServices>();
        builder.Services.AddAutoMapper(typeof(Program));

        // Adicionando CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("RegistroDeHoras.Client", policy =>
            {
                policy.WithOrigins("https://localhost:7224") 
                //policy.WithOrigins("http://localhost:5200") 
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

        // Configure the HTTP request pipeline.
        app.UseCors("RegistroDeHoras.Client");
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

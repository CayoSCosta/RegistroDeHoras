using System.Configuration;
using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Model;

namespace RegistroDeHoras.Api;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>()
            .Property(t => t.HorasUtilizadasRaw)
            .HasColumnName("HorasUtilizadas");

        modelBuilder.Entity<Tarefa>()
            .Property(t => t.HorasDePausaRaw)
            .HasColumnName("HorasDePausa");
    }

}
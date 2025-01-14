using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Model;

namespace RegistroDeHoras.Api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tarefa>? Tarefas { get; set; }
}
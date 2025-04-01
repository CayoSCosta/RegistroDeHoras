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
    public DbSet<Pausa> Pausas { get; set; }

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
        base.OnModelCreating(modelBuilder);

        // Mapeando a tabela Tarefa corretamente
        modelBuilder.Entity<Tarefa>()
            .Property(t => t.HorasUtilizadasRaw)
            .HasColumnName("HorasUtilizadasRaw");

        modelBuilder.Entity<Tarefa>()
            .Property(t => t.HorasDePausaRaw)
            .HasColumnName("HorasDePausaRaw");

        // Configuração do relacionamento entre Tarefa e Pausa
        modelBuilder.Entity<Pausa>()
            .HasOne(p => p.Tarefa)
            .WithMany(t => t.Pausas)
            .HasForeignKey(p => p.TarefaId)
            .OnDelete(DeleteBehavior.Cascade); // Cascateamento opcional se quiser excluir Pausas quando Tarefa for excluída
    }
}

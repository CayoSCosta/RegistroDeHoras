namespace RegistroDeHoras.Model;

public class Pausa : BaseModel
{
    public DateTime Inicio { get; set; }
    public DateTime Termino { get; set; }
    public TimeSpan Duracao => Termino - Inicio;

    public string? Observacao { get; set; }
        
    public Guid TarefaId { get; set; }
    public Tarefa Tarefa { get; set; } = null!;
}
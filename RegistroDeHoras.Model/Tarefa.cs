namespace RegistroDeHoras.Model;

public class Tarefa : BaseModel
{
    public DateTime Inicio { get; set; }
    public DateTime Termino { get; set; }
    public DateTime Pausa { get; set; }
    public DateTime Reinicio { get; set; }
    public TimeSpan HorasUtilizadas { get; set; }
    public TimeSpan HorasDePausa { get; set; }
    public string? NumeroTarefa { get; set; }
    public string? Titulo { get; set; }
    public string? Cliente { get; set; }
    public string? Descricao { get; set; }
    public string? StatusDaTarefa { get; set; }
}
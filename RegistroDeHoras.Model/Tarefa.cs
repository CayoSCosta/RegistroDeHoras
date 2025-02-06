using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDeHoras.Model;

public class Tarefa : BaseModel
{
    public DateTime Inicio { get; set; }
    public DateTime Termino { get; set; }
    public DateTime Pausa { get; set; }
    public DateTime Reinicio { get; set; }
    public string? NumeroAtividade { get; set; }
    public string? Titulo { get; set; }
    public string? Cliente { get; set; }
    public string? Descricao { get; set; }
    public string? StatusDaTarefa { get; set; }
    public long HorasUtilizadasRaw { get; set; }
    public long HorasDePausaRaw { get; set; }

    [NotMapped]
    public TimeSpan HorasUtilizadas
    {
        get => TimeSpan.FromSeconds(HorasUtilizadasRaw);
        set => HorasUtilizadasRaw = (long)value.TotalSeconds;
    }

    [NotMapped]
    public TimeSpan HorasDePausa
    {
        get => TimeSpan.FromSeconds(HorasDePausaRaw);
        set => HorasDePausaRaw = (long)value.TotalSeconds;
    }
}

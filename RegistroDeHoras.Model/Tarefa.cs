using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroDeHoras.Model;

public class Tarefa : BaseModel
{
    public DateTime Inicio { get; set; }
    public DateTime Termino { get; set; }
    public string? NumeroDaTarefa { get; set; }
    public string? Titulo { get; set; }
    public string? Cliente { get; set; }
    public string? Descricao { get; set; }
    public string? Solucao { get; set; }
    public string? StatusDaTarefa { get; set; }

    public long HorasUtilizadasRaw { get; set; }
    public long HorasDePausaRaw { get; set; }

    public List<Pausa> Pausas { get; set; } = new();

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

    public void CalcularHorasUtilizadas()
    {
        HorasDePausa = TimeSpan.FromSeconds(Pausas.Sum(p => p.Duracao.TotalSeconds));
        HorasUtilizadas = Termino - Inicio - HorasDePausa;
    }
}



using System;

namespace RegistroDeHoras.Model;

public class Tarefa : BaseModel
{
    public DateTime DataDeInicio { get; set; }
    public DateTime DataDePausa { get; set; }
    public DateTime DataDeReInicio { get; set; }
    public DateTime DataDeTermino { get; set; }
    public TimeSpan HorasDePausa { get; set; }
    public TimeSpan HorasTrabalhadas { get; set; }
    public string? NumeroAtividade { get; set; }
    public string? Titulo { get; set; }
    public string? Cliente { get; set; }
    public string? Descricao { get; set; }
    public string? StatusDaTarefa { get; set; }
    }
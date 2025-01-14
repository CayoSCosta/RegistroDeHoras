using System;

namespace RegistroDeHoras.Model;

public class Tarefa : BaseModel
{
    public DateOnly DataDeInicio { get; set; }
    public TimeOnly HoraDeInicio { get; set; }
    public TimeOnly HoraDeTermino { get; set; }
    public TimeOnly HorasDePausa { get; set; }
    public TimeOnly HorasUtilizadas { get; set; }
    public string? NumeroAtividade { get; set; }
    public string? Titulo { get; set; }
    public string? Cliente { get; set; }
    public string? Descricao { get; set; }
    public string? StatusDaTarefa { get; set; }
    }
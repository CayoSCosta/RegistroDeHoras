using System.ComponentModel.DataAnnotations;

namespace RegistroDeHoras.Model.ViewModels;

public class TarefaViewModel
{
    public DateTime Inicio { get; set; }

    public DateTime Termino { get; set; }

    public List<Pausa> Pausas { get; set; } = new();

    public TimeSpan HorasUtilizadas => Termino - Inicio - Pausas.Aggregate(TimeSpan.Zero, (total, pausa) => total + pausa.Duracao);

    public TimeSpan HorasDePausa => Pausas.Aggregate(TimeSpan.Zero, (total, pausa) => total + pausa.Duracao);

    [Required(ErrorMessage = "O campo Número da Atividade é obrigatório.")]
    public string? NumeroAtividade { get; set; }

    [Required(ErrorMessage = "O campo Título é obrigatório.")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "O campo Cliente é obrigatório.")]
    public string? Cliente { get; set; }

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string? Descricao { get; set; }

    public string? StatusDaTarefa { get; set; }
}

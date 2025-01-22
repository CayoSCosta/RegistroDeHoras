using System;
using System.ComponentModel.DataAnnotations;

namespace RegistroDeHoras.Model.ViewModels;

public class TarefaViewModels
{
    public DateTime Inicio { get; set; }
    public DateTime Termino { get; set; }
    public DateTime Pausa { get; set; }
    public DateTime Reinicio { get; set; }
    public TimeSpan HorasUtilizadas { get; set; }
    public TimeSpan HorasDePausa { get; set; }

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

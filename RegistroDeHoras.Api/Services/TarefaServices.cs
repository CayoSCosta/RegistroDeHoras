using RegistroDeHoras.Model;

namespace RegistroDeHoras.Api.Services;

public class TarefaServices : ITarefaServices
{
    private readonly ILogger<TarefaServices>? _logger;
    private readonly AppDbContext? _context;


    public TimeSpan CalcularHorasUtilizadas(DateTime termino, DateTime reinicio, DateTime pausa, DateTime inicio)
    {
        TimeSpan difTerminoReinicio = termino - reinicio;
        TimeSpan difPausaInicio = pausa - inicio;
        TimeSpan horasUtilizadas = difTerminoReinicio + difPausaInicio;
        return horasUtilizadas;
    }

    public TimeSpan CalcularhorasDePausa(DateTime reinicio, DateTime pausa)
    {
        TimeSpan horasDePausa = reinicio - pausa;
        return horasDePausa;
    }
}

namespace RegistroDeHoras.Api.Services;

public interface ITarefaServices
{
    public TimeSpan CalcularHorasUtilizadas(DateTime termino, DateTime reinicio, DateTime pausa, DateTime inicio);
    public TimeSpan CalcularhorasDePausa(DateTime reinicio, DateTime pausa);
}

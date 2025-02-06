using RegistroDeHoras.Model;

namespace RegistroDeHoras.Api.Services;

public interface ITarefaServices
{
    public double CalcularHorasUtilizadas(DateTime termino, DateTime reinicio, DateTime pausa, DateTime inicio);
    public TimeSpan CalcularhorasDePausa(DateTime reinicio, DateTime pausa);
    public Task<byte[]> ExportarTarefasParaExcelAsync(List<Tarefa> tarefas);
}

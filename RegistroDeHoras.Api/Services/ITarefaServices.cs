using RegistroDeHoras.Model;

namespace RegistroDeHoras.Api.Services;

public interface ITarefaServices
{
    public double CalcularHorasUtilizadas(DateTime termino, DateTime reinicio, DateTime pausa, DateTime inicio);
    public TimeSpan CalcularhorasDePausa(DateTime reinicio, DateTime pausa);
    public byte[] GerarRelatorioExcel(List<Tarefa> tarefas, DateTime dataInicio, DateTime dataFim);
}

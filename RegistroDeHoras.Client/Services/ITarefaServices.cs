using RegistroDeHoras.Model.DTOs;
using RegistroDeHoras.Model.ViewModels;

namespace RegistroDeHoras.Client.Services;

public interface ITarefasServices
{
    Task<List<TarefaViewModel>> ObterTodasTarefasAsync();
    Task<TarefaViewModel> ObterTarefaPorIdAsync(Guid id);
    Task<TarefaViewModel> CriarTarefaAsync(TarefaViewModel tarefaVM);
    Task<TarefaViewModel> PararTarefaAsync(PararTarefaRequest request);
    Task<TarefaViewModel> FinalizarTarefaAsync(string numeroDaTarefa);
    Task<bool> DeletarTarefaAsync(string numeroDaTarefa);
    Task<TarefaViewModel> EditarTarefaAsync(string numeroAtividade, TarefaViewModel tarefaVM);
    Task<List<TarefaViewModel>> ObterTarefasPorData(DateTime dataInicio, DateTime dataFim);
    Task<byte[]> ExportarTarefasParaExcelAsync(DateTime dataInicio, DateTime dataFim);
}

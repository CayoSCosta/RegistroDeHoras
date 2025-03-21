using RegistroDeHoras.Model.ViewModels;

namespace RegistroDeHoras.Client.Services;

public interface ITarefasService
{
    Task<List<TarefaViewModel>> ObterTodasTarefasAsync();
    Task<TarefaViewModel> ObterTarefaPorIdAsync(Guid id);
    Task<TarefaViewModel> CriarTarefaAsync(TarefaViewModel tarefaVM);
    Task<TarefaViewModel> PararTarefaAsync(string numeroDaTarefa, string observacao);
    Task<TarefaViewModel> FinalizarTarefaAsync(string numeroDaTarefa);
    Task<bool> DeletarTarefaAsync(Guid id);
    Task<TarefaViewModel> EditarTarefaAsync(string numeroAtividade, TarefaViewModel tarefaVM);
    Task<List<TarefaViewModel>> ObterTarefasPorData(DateTime dataInicio, DateTime dataFim);
    Task<byte[]> ExportarTarefasParaExcelAsync(DateTime dataInicio, DateTime dataFim);
}

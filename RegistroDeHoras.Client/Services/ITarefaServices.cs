
using RegistroDeHoras.Model.ViewModels;

namespace RegistroDeHoras.Client.Services;

    public interface ITarefaServices
    {
        Task<List<TarefaViewModel>> ObterTodasTarefasAsync();
        Task<TarefaViewModel> ObterTarefaPorIdAsync(Guid id);
        Task<TarefaViewModel> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade);
        Task<TarefaViewModel> PararTarefaAsync(string numeroDaTarefa);
        Task<TarefaViewModel> FinalizarTarefaAsync(string numeroDaTarefa);
        Task<bool> DeletarTarefaAsync(Guid id);
    }

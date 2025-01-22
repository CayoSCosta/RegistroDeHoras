
using RegistroDeHoras.Model.ViewModels;

namespace RegistroDeHoras.Client.Services;

    public interface ITarefaServices
    {
        Task<IEnumerable<TarefaViewModels>> ObterTodosTarefasAsync();
        Task<TarefaViewModels> ObterTarefaPorIdAsync(Guid id);
        Task<TarefaViewModels> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade);
        Task<TarefaViewModels> PararTarefaAsync(Guid id, string status);
        Task<TarefaViewModels> FinalizarTarefaAsync(Guid id);
        Task<bool> DeletarTarefaAsync(Guid id);
    }

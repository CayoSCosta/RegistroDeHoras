using RegistroDeHoras.Model;

namespace RegistroDeHoras.Client.Services;

    public interface ITarefaServices
    {
        Task<IEnumerable<Tarefa>> ObterTodosTarefasAsync();
        Task<Tarefa> ObterTarefaPorIdAsync(Guid id);
        Task<Tarefa> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade);
        Task<Tarefa> PararTarefaAsync(Guid id, string status);
        Task<Tarefa> FinalizarTarefaAsync(Guid id);
        Task<bool> DeletarTarefaAsync(Guid id);
    }

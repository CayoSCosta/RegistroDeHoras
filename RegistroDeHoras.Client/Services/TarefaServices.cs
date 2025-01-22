using System.Net.Http.Json;
using RegistroDeHoras.Model;
using RegistroDeHoras.Model.ViewModels;

namespace RegistroDeHoras.Client.Services;

    public class TarefaService : ITarefaServices
    {
        private readonly HttpClient _httpClient;

        public TarefaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TarefaViewModels>> ObterTodosTarefasAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<TarefaViewModels>>("api/Tarefa");
            return result ?? Enumerable.Empty<TarefaViewModels>();
        }

        public async Task<TarefaViewModels> ObterTarefaPorIdAsync(Guid id)
        {
            var result =  await _httpClient.GetFromJsonAsync<TarefaViewModels>($"api/Tarefa/{id}");
            return result ?? new TarefaViewModels();
        }

        public async Task<TarefaViewModels> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tarefa/Nova", new { titulo, cliente, descricao, numeroAtividade });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TarefaViewModels>();
                return result ?? new TarefaViewModels();
            }

            throw new ApplicationException("Erro ao criar a tarefa.");
        }

        public async Task<TarefaViewModels> PararTarefaAsync(Guid id, string status)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Tarefa/Parar/{id}", new { status });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TarefaViewModels>();
                return result ?? new TarefaViewModels();
            }

            throw new ApplicationException("Erro ao parar a tarefa.");
        }

        public async Task<TarefaViewModels> FinalizarTarefaAsync(Guid id)
        {
            var response = await _httpClient.PostAsync($"api/Tarefa/Finalizar/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TarefaViewModels>();
                return result ?? new TarefaViewModels();
            }

            throw new ApplicationException("Erro ao finalizar a tarefa.");
        }

        public async Task<bool> DeletarTarefaAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tarefa/Deletar/{id}");

            return response.IsSuccessStatusCode;
        }
    }

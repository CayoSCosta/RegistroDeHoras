using System.Net.Http.Json;
using RegistroDeHoras.Model;

namespace RegistroDeHoras.Client.Services;

    public class TarefaService : ITarefaServices
    {
        private readonly HttpClient _httpClient;

        public TarefaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Tarefa>> ObterTodosTarefasAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Tarefa>>("api/Tarefa");
        }

        public async Task<Tarefa> ObterTarefaPorIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Tarefa>($"api/Tarefa/{id}");
        }

        public async Task<Tarefa> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tarefa/Nova", new { titulo, cliente, descricao, numeroAtividade });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Tarefa>();
            }

            throw new ApplicationException("Erro ao criar a tarefa.");
        }

        public async Task<Tarefa> PararTarefaAsync(Guid id, string status)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Tarefa/Parar/{id}", new { status });

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Tarefa>();
            }

            throw new ApplicationException("Erro ao parar a tarefa.");
        }

        public async Task<Tarefa> FinalizarTarefaAsync(Guid id)
        {
            var response = await _httpClient.PostAsync($"api/Tarefa/Finalizar/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Tarefa>();
            }

            throw new ApplicationException("Erro ao finalizar a tarefa.");
        }

        public async Task<bool> DeletarTarefaAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tarefa/Deletar/{id}");

            return response.IsSuccessStatusCode;
        }
    }
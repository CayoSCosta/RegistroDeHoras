using System.Collections.Generic;
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

        public async Task<List<TarefaViewModel>> ObterTodosTarefasAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TarefaViewModel>>("api/Tarefa");
            return result ?? new List<TarefaViewModel>();
        }

        public async Task<TarefaViewModel> ObterTarefaPorIdAsync(Guid id)
        {
            var result =  await _httpClient.GetFromJsonAsync<TarefaViewModel>($"api/Tarefa/{id}");
            return result ?? new TarefaViewModel();
        }

        public async Task<TarefaViewModel> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Tarefa/Nova", new { titulo, cliente, descricao, numeroAtividade });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
                return result ?? new TarefaViewModel();
            }

            throw new ApplicationException("Erro ao criar a tarefa.");
        }

        public async Task<TarefaViewModel> PararTarefaAsync(Guid id, string status)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Tarefa/Parar/{id}", new { status });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
                return result ?? new TarefaViewModel();
            }

            throw new ApplicationException("Erro ao parar a tarefa.");
        }

        public async Task<TarefaViewModel> FinalizarTarefaAsync(Guid id)
        {
            var response = await _httpClient.PostAsync($"api/Tarefa/Finalizar/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
                return result ?? new TarefaViewModel();
            }

            throw new ApplicationException("Erro ao finalizar a tarefa.");
        }

        public async Task<bool> DeletarTarefaAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tarefa/Deletar/{id}");

            return response.IsSuccessStatusCode;
        }
    }

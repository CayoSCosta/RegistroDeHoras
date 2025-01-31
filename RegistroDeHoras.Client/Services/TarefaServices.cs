using RegistroDeHoras.Model.ViewModels;
using System.Net.Http.Json;

namespace RegistroDeHoras.Client.Services;

public class TarefaService : ITarefaServices
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TarefaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<TarefaViewModel>> ObterTodasTarefasAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var result = await httpClient.GetFromJsonAsync<List<TarefaViewModel>>("api/Tarefa/TodasTarefas");
        return result ?? new List<TarefaViewModel>();
    }

    public async Task<TarefaViewModel> ObterTarefaPorIdAsync(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDehoras.Api");
        var result = await httpClient.GetFromJsonAsync<TarefaViewModel>($"api/Tarefa/{id}");
        return result ?? new TarefaViewModel();
    }

    public async Task<TarefaViewModel> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDehoras.Api");
        var response = await httpClient.PostAsJsonAsync("api/Tarefa/Nova", new { titulo, cliente, descricao, numeroAtividade });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }

        throw new ApplicationException("Erro ao criar a tarefa.");
    }

    public async Task<TarefaViewModel> PararTarefaAsync(Guid id, string status)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDehoras.Api");
        var response = await httpClient.PostAsJsonAsync($"api/Tarefa/Parar/{id}", new { status });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }

        throw new ApplicationException("Erro ao parar a tarefa.");
    }

    public async Task<TarefaViewModel> FinalizarTarefaAsync(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDehoras.Api");
        var response = await httpClient.PostAsync($"api/Tarefa/Finalizar/{id}", null);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }

        throw new ApplicationException("Erro ao finalizar a tarefa.");
    }

    public async Task<bool> DeletarTarefaAsync(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDehoras.Api");
        var response = await httpClient.DeleteAsync($"api/Tarefa/Deletar/{id}");

        return response.IsSuccessStatusCode;
    }
}

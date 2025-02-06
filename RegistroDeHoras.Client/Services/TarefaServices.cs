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
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var result = await httpClient.GetFromJsonAsync<TarefaViewModel>($"api/Tarefa/{id}");
        return result ?? new TarefaViewModel();
    }

    public async Task<TarefaViewModel> CriarTarefaAsync(string titulo, string cliente, string descricao, string numeroAtividade)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.PostAsJsonAsync("api/Tarefa/Nova", new TarefaViewModel
        {
            Titulo = titulo,
            Cliente = cliente,
            Descricao = descricao,
            NumeroAtividade = numeroAtividade
        });

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }

        throw new ApplicationException("Erro ao criar a tarefa.");
    }

    public async Task<TarefaViewModel> PararTarefaAsync(string numeroDaTarefa)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.PostAsJsonAsync("api/Tarefa/Parar", numeroDaTarefa);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }
        else
        {
            var result = await response.Content.ReadAsStringAsync();
        }

        throw new ApplicationException($"Erro ao parar a tarefa. Código: {response.StatusCode}, Detalhes: {await response.Content.ReadAsStringAsync()}");
    }


    public async Task<TarefaViewModel> FinalizarTarefaAsync(string numeroDaTarefa)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.PostAsJsonAsync($"api/Tarefa/Finalizar", numeroDaTarefa);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }
        else
        {
            var result = await response.Content.ReadAsStringAsync();
        }

        throw new ApplicationException("Erro ao finalizar a tarefa.");
    }

    public async Task<bool> DeletarTarefaAsync(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.DeleteAsync($"api/Tarefa/Deletar/{id}");

        return response.IsSuccessStatusCode;
    }

    public async Task<List<TarefaViewModel>> ObterTarefasPorData(DateTime dataInicio, DateTime dataFim)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var result = await httpClient.GetFromJsonAsync<List<TarefaViewModel>>($"api/Tarefa/PorData?dataInicio={dataInicio:yyyy-MM-dd}&dataFim={dataFim:yyyy-MM-dd}");

        return result ?? new List<TarefaViewModel>();
    }

}

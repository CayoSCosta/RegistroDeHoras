using RegistroDeHoras.Model.ViewModels;
using System.Net.Http.Json;
using RegistroDeHoras.Model.DTOs;
using Microsoft.Extensions.Logging;

namespace RegistroDeHoras.Client.Services;

public class TarefasServices : ITarefasServices
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<TarefasServices> _logger;

    public TarefasServices(IHttpClientFactory httpClientFactory, ILogger<TarefasServices> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<List<TarefaViewModel>> ObterTodasTarefasAsync()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
            var response = await httpClient.GetAsync("api/Tarefa/TodasTarefas");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<TarefaViewModel>();
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<List<TarefaViewModel>>();
            return result ?? new List<TarefaViewModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter todas as tarefas");
            throw new ApplicationException("Erro ao obter todas as tarefas", ex);
        }
    }

    public async Task<TarefaViewModel> ObterTarefaPorIdAsync(Guid id)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var result = await httpClient.GetFromJsonAsync<TarefaViewModel>($"api/Tarefa/{id}");
        return result ?? new TarefaViewModel();
    }

    public async Task<TarefaViewModel> CriarTarefaAsync(TarefaViewModel tarefaVM)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
            var response = await httpClient.PostAsJsonAsync("api/Tarefa/Nova", tarefaVM);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
                return result ?? new TarefaViewModel();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Erro ao criar a tarefa. Código: {StatusCode}, Detalhes: {ErrorContent}", response.StatusCode, errorContent);
                throw new ApplicationException($"Erro ao criar a tarefa. Código: {response.StatusCode}, Detalhes: {errorContent}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, "Erro de solicitação HTTP ao criar a tarefa");
            throw new ApplicationException("Erro de solicitação HTTP ao criar a tarefa", httpEx);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar a tarefa");
            throw new ApplicationException("Erro ao criar a tarefa", ex);
        }
    }
    
    public async Task<TarefaViewModel> PararTarefaAsync(PararTarefaRequest request)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.PostAsJsonAsync("api/Tarefa/Parar", request);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }

        throw new ApplicationException($"Erro ao parar a tarefa. Código: {response.StatusCode}, Detalhes: {await response.Content.ReadAsStringAsync()}");
    }


    public async Task<TarefaViewModel> FinalizarTarefaAsync(string numeroDaTarefa)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.PostAsJsonAsync("api/Tarefa/Finalizar", numeroDaTarefa);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }

        throw new ApplicationException("Erro ao finalizar a tarefa.");
    }

    public async Task<bool> DeletarTarefaAsync(string numeroDaTarefa)
    {
        if (string.IsNullOrWhiteSpace(numeroDaTarefa))
            throw new ArgumentException("O número da tarefa não pode ser nulo ou vazio.", nameof(numeroDaTarefa));

        try
        {
            var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
            var response = await httpClient.DeleteAsync($"api/Tarefa/Deletar/{numeroDaTarefa}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"Erro ao deletar tarefa {numeroDaTarefa}: {response.StatusCode} - {errorMessage}");
                return false;
            }

            return true;
        }
        catch (HttpRequestException ex)
        {
            Console.Error.WriteLine($"Erro de conexão ao tentar deletar a tarefa {numeroDaTarefa}: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro inesperado ao deletar a tarefa {numeroDaTarefa}: {ex.Message}");
            throw; // Relança a exceção para permitir um tratamento mais adequado no nível superior
        }
    }


    public async Task<TarefaViewModel> EditarTarefaAsync(string numeroAtividade, TarefaViewModel tarefaVM)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.PutAsJsonAsync($"api/Tarefa/EditarPorNumero/{numeroAtividade}", tarefaVM);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<TarefaViewModel>();
            return result ?? new TarefaViewModel();
        }

        throw new ApplicationException($"Erro ao editar a tarefa. Código: {response.StatusCode}, Detalhes: {await response.Content.ReadAsStringAsync()}");
    }

    public async Task<List<TarefaViewModel>> ObterTarefasPorData(DateTime dataInicio, DateTime dataFim)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var result = await httpClient.GetFromJsonAsync<List<TarefaViewModel>>($"api/Tarefa/PorData?dataInicio={dataInicio:yyyy-MM-dd}&dataFim={dataFim:yyyy-MM-dd}");

        return result ?? new List<TarefaViewModel>();
    }

    public async Task<byte[]> ExportarTarefasParaExcelAsync(DateTime dataInicio, DateTime dataFim)
    {
        var httpClient = _httpClientFactory.CreateClient("RegistroDeHoras.Api");
        var response = await httpClient.GetAsync($"api/Tarefa/exportar?dataInicio={dataInicio:yyyy-MM-dd}&dataFim={dataFim:yyyy-MM-dd}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }

        throw new ApplicationException($"Erro ao exportar tarefas. Código: {response.StatusCode}, Detalhes: {await response.Content.ReadAsStringAsync()}");
    }
}

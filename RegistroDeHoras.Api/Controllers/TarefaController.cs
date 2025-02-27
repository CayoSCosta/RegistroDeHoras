using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Api;
using RegistroDeHoras.Api.Services;
using RegistroDeHoras.Model;
using RegistroDeHoras.Model.ViewModels;

namespace TarefaDeHoras.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ILogger<TarefaController>? _logger;
    private readonly AppDbContext _context;
    private readonly ITarefaServices _tarefaServices;
    private readonly IMapper _mapper;

    public TarefaController(ILogger<TarefaController>? logger, AppDbContext context, ITarefaServices tarefaServices, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _tarefaServices = tarefaServices;
        _mapper = mapper;
    }

    [HttpGet("TodasTarefas")]
    public async Task<ActionResult<List<TarefaViewModel>>> ObterTodosTarefasAsync()
    {
        try
        {
            if (_context.Tarefas == null)
                return NotFound("Nenhuma tarefa encontrada");

            var listaDetarefas = await _context.Tarefas.ToListAsync();
            List<TarefaViewModel> listaDeTarefasViewModel = new();
            foreach (var tarefa in listaDetarefas)
            {
                TarefaViewModel tarefaViewModel = new()
                {
                    Inicio = tarefa.Inicio,
                    Termino = tarefa.Termino,
                    Pausa = tarefa.Pausa,
                    Reinicio = tarefa.Reinicio,
                    HorasUtilizadas = tarefa.HorasUtilizadas,
                    HorasDePausa = tarefa.HorasDePausa,
                    NumeroAtividade = tarefa.NumeroAtividade,
                    Titulo = tarefa.Titulo,
                    Cliente = tarefa.Cliente,
                    Descricao = tarefa.Descricao,
                    StatusDaTarefa = tarefa.StatusDaTarefa,
                };

                listaDeTarefasViewModel.Add(tarefaViewModel);
            }

            return Ok(listaDeTarefasViewModel);
        }
        catch (Exception)
        {

            throw;
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Tarefa>> ObterTarefaPorIdAsync(Guid id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
            return NotFound();

        return Ok(tarefa);
    }

    [HttpPost("Nova")]
    [ProducesResponseType(typeof(TarefaViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CriarTarefa([FromBody] TarefaViewModel tarefaVM)
    {
        _logger?.LogInformation("Iniciando criação de uma nova tarefa com Titulo: {Titulo}", tarefaVM.Titulo);

        var tarefa = new Tarefa
        {
            Titulo = tarefaVM.Titulo,
            Cliente = tarefaVM.Cliente,
            Descricao = tarefaVM.Descricao,
            NumeroAtividade = tarefaVM.NumeroAtividade,
            StatusDaTarefa = "Em andamento",
            Inicio = DateTime.Now,
        };

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(tarefaVM);
    }

    [HttpPost("Parar")]
    public async Task<IActionResult> PararTarefa([FromBody] string numeroDaTarefa)
    {
        var tarefa = await _context.Tarefas
            .FirstOrDefaultAsync(t => t.NumeroAtividade == numeroDaTarefa);

        if (tarefa == null)
            return NotFound();

        if (tarefa.StatusDaTarefa == "Em andamento")
        {
            tarefa.Pausa = DateTime.Now;
            tarefa.StatusDaTarefa = "Parada";
        }
        else if (tarefa.StatusDaTarefa == "Parada")
        {
            tarefa.Reinicio = DateTime.Now;
            tarefa.StatusDaTarefa = "Em andamento";
        }
        else
        {
            return BadRequest();
        }

        _context.Update(tarefa);
        await _context.SaveChangesAsync();

        return Ok(tarefa);
    }

    [HttpPost("Finalizar")]
    public async Task<IActionResult> FinalizarTarefa([FromBody] string numeroDaTarefa)
    {
        try
        {
            var tarefa = await _context.Tarefas
                .FirstOrDefaultAsync(t => t.NumeroAtividade == numeroDaTarefa);

            if (tarefa == null)
                return NotFound();

            tarefa.Termino = DateTime.Now;
            double horasUtilizadas = _tarefaServices.CalcularHorasUtilizadas(tarefa.Termino, tarefa.Reinicio, tarefa.Pausa, tarefa.Inicio);
            tarefa.HorasUtilizadas = TimeSpan.FromHours(horasUtilizadas);
            tarefa.HorasDePausa = _tarefaServices.CalcularhorasDePausa(tarefa.Reinicio, tarefa.Pausa);
            tarefa.StatusDaTarefa = "Finalizada";

            _context.Update(tarefa);
            await _context.SaveChangesAsync();

            return Ok(tarefa);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    [HttpDelete("Deletar/{id}")]
    public IEnumerable<Tarefa> DeletarTarefaTarefa(Guid Id)
    {
        throw new NotImplementedException();
    }

    [HttpGet("exportar")]
    public async Task<IActionResult> ExportarParaExcel(DateTime? dataInicio, DateTime? dataFim)
    {
        dataInicio ??= DateTime.MinValue;
        dataFim ??= DateTime.MaxValue;

        var tarefas = await _context.Tarefas
            .Where(t => t.Inicio >= dataInicio && t.Termino <= dataFim)
            .ToListAsync();

        if (tarefas == null || tarefas.Count == 0)
            return BadRequest("Nenhuma tarefa encontrada para exportação.");

        var arquivoExcel = await _tarefaServices.ExportarTarefasParaExcelAsync(tarefas);
        return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"horas_{dataInicio.Value.ToString("yyyyMMdd")}_{dataFim.Value.ToString("yyyyMMdd")}.xlsx");
    }

}
//Exemplo de conversão com automapper de viewmodel para entidade
//[HttpPost]
//    public async Task<IActionResult> CriarTarefa([FromBody] TarefaViewModel viewModel)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest(ModelState);

//        // Fazendo o mapeamento da ViewModel para a Entidade
//        var tarefa = _mapper.Map<Tarefa>(viewModel);

//        _context.Tarefas.Add(tarefa);
//        await _context.SaveChangesAsync();

//        return Ok(tarefa);
//    }


//Exemplo com listas
//[HttpGet]
//    public async Task<ActionResult<IEnumerable<TarefaViewModel>>> ObterTodasTarefas()
//    {
//        var tarefas = await _context.Tarefas.ToListAsync();

//        // Mapeia a lista de Entidades para ViewModels
//        var viewModels = _mapper.Map<List<TarefaViewModel>>(tarefas);

//        return Ok(viewModels);
//    }

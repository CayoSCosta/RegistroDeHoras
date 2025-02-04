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
        if (_context.Tarefas == null)
            return NotFound("Nenhuma tarefa encontrada");

        var listaDetarefas =  await _context.Tarefas.ToListAsync();
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
            Inicio = DateTime.Now,
        };

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Não precisaria mapear, porque já está no formato de ViewModel
        return Ok(tarefaVM);
    }


    [HttpPost("Parar/{id}")]
    public async Task<IActionResult> PararTarefa(Guid id, string status)
    {
        var tarefa = _context.Tarefas.Find(id);

        if (tarefa == null)
            return NotFound();

        if (tarefa.StatusDaTarefa == "Em adamento")
        {
            tarefa.Pausa = DateTime.Now;
            tarefa.HorasDePausa = tarefa.HorasDePausa + (tarefa.Pausa - tarefa.Inicio);
            tarefa.StatusDaTarefa = "Parada";
        }
        else if (tarefa.StatusDaTarefa == "Parada")
        {
            tarefa.Reinicio = DateTime.Now;
            tarefa.StatusDaTarefa = "Em adamento";
        }
        else
        {
            return BadRequest();
        }

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        return Ok(tarefa);
    }

    [HttpPost("Finalizar/{id}")]
    public async Task<IActionResult> FinalizarTarefa(Guid id)
    {
        var tarefa = _context.Tarefas.Find(id);

        if (tarefa == null)
            return NotFound();

        tarefa.Termino = DateTime.Now;
        tarefa.HorasUtilizadas = _tarefaServices.CalcularHorasUtilizadas(tarefa.Termino, tarefa.Reinicio, tarefa.Pausa, tarefa.Inicio);
        tarefa.HorasDePausa = _tarefaServices.CalcularhorasDePausa(tarefa.Reinicio, tarefa.Pausa);
        tarefa.StatusDaTarefa = "Finalizada";

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        return Ok(tarefa);
    }

    [HttpDelete("Deletar/{id}")]
    public IEnumerable<Tarefa> DeletarTarefaTarefa(Guid Id)
    {
        throw new NotImplementedException();
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

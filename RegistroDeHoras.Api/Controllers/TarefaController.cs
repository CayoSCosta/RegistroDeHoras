using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Api;
using RegistroDeHoras.Api.Services;
using RegistroDeHoras.Model;

namespace TarefaDeHoras.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ILogger<TarefaController>? _logger;
    private readonly AppDbContext _context;
    private readonly ITarefaServices _tarefaServices;

    public TarefaController(ILogger<TarefaController>? logger, AppDbContext context, ITarefaServices tarefaServices)
    {
        _logger = logger;
        _context = context;
        _tarefaServices = tarefaServices;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTodosTarefasAsync()
    {
        if (_context.Tarefas == null)
            return NotFound("Nenhuma tarefa encontrada");

        return await _context.Tarefas.ToListAsync();
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
    [ProducesResponseType(typeof(Tarefa), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CriarTarefa(string titulo, string cliente, string descricao, string numeroAtividade)
    {
        _logger?.LogInformation("Iniciando criação de uma nova tarefa com Titulo: {Titulo}", titulo);

        var tarefa = new Tarefa
        {
            Titulo = titulo,
            Cliente = cliente,
            Descricao = descricao,
            NumeroAtividade = numeroAtividade,
            Inicio = DateTime.Now,
        };

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        // Verificar se a rota está funcionando corretamente
        //return CreatedAtAction(nameof(ObterTarefaPorIdAsync), new { id = tarefa.Id }, tarefa);
        return Ok(tarefa);
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


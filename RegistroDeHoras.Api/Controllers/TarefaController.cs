using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Api;
using RegistroDeHoras.Model;

namespace TarefaDeHoras.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TarefaController : ControllerBase
{
    private readonly ILogger<TarefaController> _logger;
    private readonly AppDbContext _context;

    public TarefaController(ILogger<TarefaController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tarefa>>> ObterTodosTarefasAsync()
    {
        if (_context.Tarefas == null)
            return NotFound("Nenhuma tarefa encontrada");

        return await _context.Tarefas.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tarefa>> ObterTarefaPorIdAsync(Guid id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
            return NotFound();

        return tarefa;
    }

    [HttpPost]
    public IActionResult CriarTarefa(string Titulo, string Cliente, string Descricao, string NumeroAtividade)
    {
        var tarefa = new Tarefa
        {
            Titulo = Titulo,
            Cliente = Cliente,
            Descricao = Descricao,
            NumeroAtividade = NumeroAtividade,
            Inicio = DateTime.Now,
        };

        _context.Tarefas.Add(tarefa);
        _context.SaveChanges();

        return CreatedAtAction(nameof(ObterTarefaPorIdAsync), new { id = tarefa.ID }, tarefa);
    }

    [HttpPost]
    public async Task<IActionResult> PararTarefa(Guid id, string status)
    {
        var tarefa = _context.Tarefas.Find(id);

        if (tarefa == null)
            return NotFound();

        if(tarefa.StatusDaTarefa == "Em adamento")
        {   
            tarefa.Pausa = DateTime.Now;
            tarefa.StatusDaTarefa = "Parada";
        }
        else if(tarefa.StatusDaTarefa == "Parada")
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

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IEnumerable<Tarefa> DeletarTarefaTarefa(Guid Id)
    {
        throw new NotImplementedException();
    }
}


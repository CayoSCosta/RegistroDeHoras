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
    public async Task<ActionResult> CriarTarefa(string Titulo, string Cliente, string Descricao, string NumeroAtividade)
    {
        var tarefa = new Tarefa
        {
            Titulo = Titulo,
            Cliente = Cliente,
            Descricao = Descricao,
            NumeroAtividade = NumeroAtividade,
            DataDeInicio = DateTime.Now
        };

       await _context.Tarefas.AddAsync(tarefa);
       await  _context.SaveChangesAsync();

        return CreatedAtAction(nameof(ObterTarefaPorIdAsync), new { id = tarefa.ID }, tarefa);
    }

    [HttpPost]
    public async Task<ActionResult<Tarefa>> PararTarefa(Guid id, string status)
    {
        var tarefa = _context.Tarefas.Find(id);

        if (tarefa == null)
            return NotFound();

        //reiniciar
        if(tarefa.StatusDaTarefa == "Parada")
        {

        }
        //pausar
        else if(tarefa.StatusDaTarefa == "Em andamento")
        {            
        }
        else
            return BadRequest();           

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        return Ok(tarefa);
    }

    [HttpDelete("{id}")]
    public IEnumerable<Tarefa> DeletarTarefaTarefa(Guid Id)
    {
        throw new NotImplementedException();
    }
}


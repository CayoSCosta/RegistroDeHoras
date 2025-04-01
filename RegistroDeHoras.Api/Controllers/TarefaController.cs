#region antigo
// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using RegistroDeHoras.Api;
// using RegistroDeHoras.Api.Services;
// using RegistroDeHoras.Model;
// using RegistroDeHoras.Model.ViewModels;

// namespace TarefaDeHoras.Api.Controllers;

// [Route("api/[controller]")]
// [ApiController]
// public class TarefaController : ControllerBase
// {
//     private readonly ILogger<TarefaController>? _logger;
//     private readonly AppDbContext _context;
//     private readonly ITarefaServices _tarefaServices;
//     private readonly IMapper _mapper;

//     public TarefaController(ILogger<TarefaController>? logger, AppDbContext context, ITarefaServices tarefaServices, IMapper mapper)
//     {
//         _logger = logger;
//         _context = context;
//         _tarefaServices = tarefaServices;
//         _mapper = mapper;
//     }

//     [HttpGet("TodasTarefas")]
//     public async Task<ActionResult<List<TarefaViewModel>>> ObterTodosTarefasAsync()
//     {
//         try
//         {
//             if (_context.Tarefas == null)
//                 return NotFound("Nenhuma tarefa encontrada");

//             var listaDetarefas = await _context.Tarefas
//                 .Include(t => t.Pausas) // Inclui as pausas associadas às tarefas
//                 .Where(t => t.Pausas.Any()) // Filtra apenas as tarefas que possuem pausas
//                 .ToListAsync();

//             if (listaDetarefas == null || listaDetarefas.Count == 0)
//                 return NotFound("Nenhuma tarefa com pausas encontrada");

//             // Mapeia a lista de Entidades para ViewModels usando AutoMapper
//             var listaDeTarefasViewModel = _mapper.Map<List<TarefaViewModel>>(listaDetarefas);

//             return Ok(listaDeTarefasViewModel);
//         }
//         catch (Exception ex)
//         {
//             _logger?.LogError(ex, "Erro ao obter todas as tarefas");
//             return StatusCode(500, "Erro interno ao obter todas as tarefas");
//         }
//     }

//     [HttpGet("{id}")]
//     public async Task<ActionResult<TarefaViewModel>> ObterTarefaPorIdAsync(Guid id)
//     {
//         _logger?.LogInformation("Buscando tarefa com ID: {Id}", id);

//         var tarefa = await _context.Tarefas
//             .Include(t => t.Pausas)
//             .FirstOrDefaultAsync(t => t.Id == id);

//         if (tarefa == null)
//         {
//             _logger?.LogWarning("Tarefa com ID {Id} não encontrada.", id);
//             return NotFound();
//         }

//         var tarefaViewModel = _mapper.Map<TarefaViewModel>(tarefa);
//         return Ok(tarefaViewModel);
//     }

//     [HttpPost("Nova")]
//     [ProducesResponseType(typeof(TarefaViewModel), StatusCodes.Status201Created)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     public async Task<ActionResult> CriarTarefa([FromBody] TarefaViewModel tarefaVM)
//     {
//         try
//         {
//             _logger?.LogInformation("Iniciando criação de uma nova tarefa com Título: {Titulo}", tarefaVM.Titulo);

//             if (!ModelState.IsValid)
//             {
//                 _logger?.LogWarning("Dados inválidos para criação da tarefa.");
//                 return BadRequest(ModelState);
//             }

//             // Mapeando ViewModel para entidade
//             var tarefa = _mapper.Map<Tarefa>(tarefaVM);
//             tarefa.StatusDaTarefa = "Em andamento";
//             tarefa.Inicio = DateTime.Now;

//             await _context.Tarefas.AddAsync(tarefa);
//             await _context.SaveChangesAsync();

//             // Verificar se o ID foi gerado corretamente
//             if (tarefa.Id == Guid.Empty)
//             {
//                 _logger?.LogError("Erro ao gerar ID para a nova tarefa.");
//                 return StatusCode(500, "Erro interno: ID gerado inválido.");
//             }

//             var tarefaCriadaVM = _mapper.Map<TarefaViewModel>(tarefa);

//             // Testar se a URL da rota está correta
//             var url = Url.Action(nameof(ObterTarefaPorIdAsync), "Tarefa", new { id = tarefa.Id }, Request.Scheme);
//             //var url = $"{Request.Scheme}://{Request.Host}/api/tarefas/{tarefa.Id}";

//             if (url == null)
//             {
//                 _logger?.LogError("Falha ao gerar URL para a nova tarefa.");
//                 return StatusCode(500, "Erro interno ao criar a URL da nova tarefa.");
//             }

//             _logger?.LogInformation("Tarefa criada com sucesso. ID: {Id}, URL: {Url}", tarefa.Id, url);

//             // Retorna a URL correta
//             return CreatedAtAction(nameof(ObterTarefaPorIdAsync), new { id = tarefa.Id.ToString() }, tarefaCriadaVM);
//         }
//         catch (Exception ex)
//         {
//             _logger?.LogError(ex, "Erro ao criar a tarefa.");
//             return StatusCode(500, "Erro interno ao criar a tarefa.");
//         }
//     }


//     [HttpPost("Parar")]
//     public async Task<IActionResult> PararTarefa([FromBody] string numeroDaTarefa, string observacao)
//     {
//         var tarefa = await _context.Tarefas
//             .Include(t => t.Pausas) // Inclui as pausas associadas à tarefa
//             .FirstOrDefaultAsync(t => t.NumeroDaAtividade == numeroDaTarefa);

//         if (tarefa == null)
//             return NotFound();

//         if (tarefa.StatusDaTarefa == "Em andamento")
//         {
//             // Adiciona uma nova pausa
//             var novaPausa = new Pausa
//             {
//                 Inicio = DateTime.Now,
//                 TarefaId = tarefa.Id,
//                 Observacao = observacao
//             };
//             tarefa.Pausas.Add(novaPausa);
//             tarefa.StatusDaTarefa = "Parada";
//         }
//         else
//         {
//             return BadRequest();
//         }

//         _context.Update(tarefa);
//         await _context.SaveChangesAsync();

//         return Ok(tarefa);
//     }

//     [HttpPost("Finalizar")]
//     public async Task<IActionResult> FinalizarTarefa([FromBody] string numeroDaTarefa)
//     {
//         try
//         {
//             var tarefa = await _context.Tarefas
//                 .Include(t => t.Pausas) // Inclui as pausas associadas à tarefa
//                 .FirstOrDefaultAsync(t => t.NumeroDaAtividade == numeroDaTarefa);

//             if (tarefa == null)
//                 return NotFound();

//             // Finaliza a última pausa
//             var ultimaPausa = tarefa.Pausas.LastOrDefault(p => p.Termino == default);
//             if (ultimaPausa != null)
//             {
//                 ultimaPausa.Termino = DateTime.Now;
//                 ultimaPausa.Observacao = "Tarefa finalizada";
//             }

//             tarefa.Termino = DateTime.Now;
//             tarefa.StatusDaTarefa = "Finalizada";

//             // Calcula as horas utilizadas
//             tarefa.CalcularHorasUtilizadas();

//             _context.Update(tarefa);
//             await _context.SaveChangesAsync();

//             // Mapeia a entidade Tarefa para TarefaViewModel usando AutoMapper
//             var tarefaViewModel = _mapper.Map<TarefaViewModel>(tarefa);

//             return Ok(tarefaViewModel);
//         }
//         catch (Exception ex)
//         {
//             _logger?.LogError(ex, "Erro ao finalizar a tarefa com Número da Atividade: {NumeroDaAtividade}", numeroDaTarefa);
//             return StatusCode(500, "Erro interno ao finalizar a tarefa");
//         }
//     }

//     [HttpPut("Editar/{numeroAtividade}")]
//     [ProducesResponseType(typeof(TarefaViewModel), StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status400BadRequest)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     public async Task<ActionResult> EditarTarefa(string numeroAtividade, [FromBody] TarefaViewModel tarefaVM)
//     {
//         _logger?.LogInformation("Iniciando edição da tarefa com Número da Atividade: {NumeroDaAtividade}", numeroAtividade);

//         // Buscar a tarefa pelo Número da Atividade
//         var tarefa = await _context.Tarefas
//                                    .Include(t => t.Pausas) // Inclui as pausas associadas à tarefa
//                                    .FirstOrDefaultAsync(t => t.NumeroDaAtividade == numeroAtividade);

//         if (tarefa == null)
//         {
//             return NotFound(new { Message = "Tarefa não encontrada." });
//         }

//         // Mapeia os dados da ViewModel para a entidade Tarefa
//         _mapper.Map(tarefaVM, tarefa);

//         // Atualizar os dados
//         tarefa.Titulo = tarefaVM.Titulo;
//         tarefa.Cliente = tarefaVM.Cliente;
//         tarefa.Descricao = tarefaVM.Descricao;
//         tarefa.StatusDaTarefa = tarefaVM.StatusDaTarefa;

//         if (!ModelState.IsValid)
//             return BadRequest(ModelState);

//         await _context.SaveChangesAsync();

//         return Ok(tarefaVM);
//     }

//     [HttpDelete("Deletar/{id}")]
//     [ProducesResponseType(StatusCodes.Status200OK)]
//     [ProducesResponseType(StatusCodes.Status404NotFound)]
//     public async Task<IActionResult> DeletarTarefa(Guid id)
//     {
//         _logger?.LogInformation("Iniciando exclusão da tarefa com ID: {Id}", id);

//         // Buscar a tarefa pelo ID
//         var tarefa = await _context.Tarefas
//                                    .Include(t => t.Pausas) // Inclui as pausas associadas à tarefa
//                                    .FirstOrDefaultAsync(t => t.Id == id);

//         if (tarefa == null)
//         {
//             return NotFound(new { Message = "Tarefa não encontrada." });
//         }

//         // Remover a tarefa
//         _context.Tarefas.Remove(tarefa);
//         await _context.SaveChangesAsync();

//         return Ok(new { Message = "Tarefa excluída com sucesso." });
//     }

//     [HttpGet("exportar")]
//     public async Task<IActionResult> ExportarParaExcel(DateTime? dataInicio, DateTime? dataFim)
//     {
//         dataInicio ??= DateTime.MinValue;
//         dataFim ??= DateTime.MaxValue;

//         var tarefas = await _context.Tarefas
//             .Where(t => t.Inicio >= dataInicio && t.Termino <= dataFim)
//             .Include(t => t.Pausas) // Inclui as pausas associadas às tarefas
//             .ToListAsync();

//         if (tarefas == null || tarefas.Count == 0)
//             return BadRequest("Nenhuma tarefa encontrada para exportação.");

//         var arquivoExcel = this._tarefaServices.GerarRelatorioExcel(tarefas, dataInicio.Value, dataFim.Value);
//         return File(arquivoExcel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
//             $"horas_{dataInicio.Value:yyyyMMdd}_{dataFim.Value:yyyyMMdd}.xlsx");
//     }

// }

// //Exemplo de conversão com automapper de viewmodel para entidade
// //[HttpPost]
// //    public async Task<IActionResult> CriarTarefa([FromBody] TarefaViewModel viewModel)
// //    {
// //        if (!ModelState.IsValid)
// //            return BadRequest(ModelState);

// //        // Fazendo o mapeamento da ViewModel para a Entidade
// //        var tarefa = _mapper.Map<Tarefa>(viewModel);

// //        _context.Tarefas.Add(tarefa);
// //        await _context.SaveChangesAsync();

// //        return Ok(tarefa);
// //    }


// //Exemplo com listas
// //[HttpGet]
// //    public async Task<ActionResult<IEnumerable<TarefaViewModel>>> ObterTodasTarefas()
// //    {
// //        var tarefas = await _context.Tarefas.ToListAsync();

// //        // Mapeia a lista de Entidades para ViewModels
// //        var viewModels = _mapper.Map<List<TarefaViewModel>>(tarefas);

// //        return Ok(viewModels);
// //    }
#endregion

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroDeHoras.Api;
using RegistroDeHoras.Api.Services;
using RegistroDeHoras.Model;
using RegistroDeHoras.Model.DTOs;
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
        var tarefas = await _context.Tarefas.Include(t => t.Pausas).ToListAsync();
        if (tarefas.Any())
        {
            return Ok(_mapper.Map<List<TarefaViewModel>>(tarefas.OrderByDescending(t => t.Inicio)));
        }
        else
        {
            return NotFound("Nenhuma tarefa encontrada.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TarefaViewModel>> ObterTarefaPorIdAsync(Guid id)
    {
        var tarefa = await _context.Tarefas.Include(t => t.Pausas).FirstOrDefaultAsync(t => t.Id == id);
        return tarefa == null ? NotFound() : Ok(_mapper.Map<TarefaViewModel>(tarefa));
    }

    [HttpPost("Nova")]
    public async Task<ActionResult> CriarTarefa([FromBody] TarefaViewModel tarefaVM)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var tarefa = _mapper.Map<Tarefa>(tarefaVM);
        tarefa.StatusDaTarefa = "Em andamento";
        tarefa.Inicio = DateTime.Now;

        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();

        var url = $"{Request.Scheme}://{Request.Host}/api/tarefas/{tarefa.Id}";

        return Created(url, _mapper.Map<TarefaViewModel>(tarefa));
    }

    [HttpPost("Parar")]
    public async Task<IActionResult> PararTarefa([FromBody] PararTarefaRequest request)
    {
        var tarefa = await _context.Tarefas.Include(t => t.Pausas).FirstOrDefaultAsync(t => t.NumeroAtividade == request.NumeroDaTarefa);
        if (tarefa == null) return NotFound();

        tarefa.Pausas.Add(new Pausa { Inicio = DateTime.Now, TarefaId = tarefa.Id, Observacao = request.Observacao });
        tarefa.StatusDaTarefa = "Parada";

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("Editar/{numeroAtividade}")]
    public async Task<ActionResult> EditarTarefa(string numeroAtividade, [FromBody] TarefaViewModel tarefaVM)
    {
        var tarefa = await _context.Tarefas.Include(t => t.Pausas).FirstOrDefaultAsync(t => t.NumeroAtividade == numeroAtividade);
        if (tarefa == null) return NotFound("Tarefa não encontrada.");

        _mapper.Map(tarefaVM, tarefa);
        await _context.SaveChangesAsync();
        return Ok(tarefaVM);
    }

    [HttpDelete("Deletar/{id}")]
    public async Task<IActionResult> DeletarTarefa(Guid id)
    {
        var tarefa = await _context.Tarefas.Include(t => t.Pausas).FirstOrDefaultAsync(t => t.Id == id);
        if (tarefa == null) return NotFound("Tarefa não encontrada.");

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return Ok("Tarefa excluída com sucesso.");
    }
}

using OfficeOpenXml;
using OfficeOpenXml.Style;
using RegistroDeHoras.Model;

namespace RegistroDeHoras.Api.Services;

public class TarefaServices : ITarefaServices
{
    private readonly ILogger<TarefaServices>? _logger;
    private readonly AppDbContext? _context;

    public TarefaServices(ILogger<TarefaServices>? logger, AppDbContext? context)
    {
        _logger = logger;
        _context = context;
    }

    public double CalcularHorasUtilizadas(DateTime termino, DateTime reinicio, DateTime pausa, DateTime inicio)
    {
        TimeSpan difTerminoReinicio = termino - reinicio;
        TimeSpan difPausaInicio = pausa - inicio;
        TimeSpan horasUtilizadas = difTerminoReinicio + difPausaInicio;

        return horasUtilizadas.TotalHours;
    }

    public TimeSpan CalcularhorasDePausa(DateTime reinicio, DateTime pausa)
    {
        TimeSpan horasDePausa = reinicio - pausa;
        return horasDePausa;
    }

    public byte[] GerarRelatorioExcel(List<Tarefa> tarefas, DateTime dataInicio, DateTime dataFim)
    {
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add("Relatório de Tarefas");

        // Cabeçalhos
        worksheet.Cells[1, 1].Value = "Número da Atividade";
        worksheet.Cells[1, 2].Value = "Título";
        worksheet.Cells[1, 3].Value = "Cliente";
        worksheet.Cells[1, 4].Value = "Descrição";
        worksheet.Cells[1, 5].Value = "Início";
        worksheet.Cells[1, 6].Value = "Término";
        worksheet.Cells[1, 7].Value = "Horas Utilizadas";
        worksheet.Cells[1, 8].Value = "Horas de Pausa";
        worksheet.Cells[1, 9].Value = "Status";

        // Dados
        for (int i = 0; i < tarefas.Count; i++)
        {
            var tarefa = tarefas[i];
            worksheet.Cells[i + 2, 1].Value = tarefa.NumeroDaTarefa;
            worksheet.Cells[i + 2, 2].Value = tarefa.Titulo;
            worksheet.Cells[i + 2, 3].Value = tarefa.Cliente;
            worksheet.Cells[i + 2, 4].Value = tarefa.Descricao;
            worksheet.Cells[i + 2, 5].Value = tarefa.Inicio.ToString("dd/MM/yyyy HH:mm");
            worksheet.Cells[i + 2, 6].Value = tarefa.Termino.ToString("dd/MM/yyyy HH:mm");
            worksheet.Cells[i + 2, 7].Value = tarefa.HorasUtilizadas.ToString(@"hh\:mm\:ss");
            worksheet.Cells[i + 2, 8].Value = tarefa.HorasDePausa.ToString(@"hh\:mm\:ss");
            worksheet.Cells[i + 2, 9].Value = tarefa.StatusDaTarefa;
        }

        // Ajusta a largura das colunas
        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

        return package.GetAsByteArray();
    }

}

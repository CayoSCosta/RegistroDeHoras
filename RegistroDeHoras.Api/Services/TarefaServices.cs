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

    public async Task<byte[]> ExportarTarefasParaExcelAsync(List<Tarefa> tarefas)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Tarefas");

            worksheet.Cells[1, 1].Value = "Ticket";
            worksheet.Cells[1, 2].Value = "Cliente";
            worksheet.Cells[1, 3].Value = "Título";
            worksheet.Cells[1, 4].Value = "Início";
            worksheet.Cells[1, 5].Value = "Fim";
            worksheet.Cells[1, 6].Value = "Horas Utilizadas";

            using (var range = worksheet.Cells["A1:F1"])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            int linha = 2;
            foreach (var tarefa in tarefas)
            {
                worksheet.Cells[linha, 1].Value = tarefa.NumeroAtividade;
                worksheet.Cells[linha, 2].Value = tarefa.Cliente;
                worksheet.Cells[linha, 3].Value = tarefa.Titulo;
                worksheet.Cells[linha, 4].Value = tarefa.Inicio;
                worksheet.Cells[linha, 5].Value = tarefa.Termino;
                worksheet.Cells[linha, 6].Value = tarefa.HorasUtilizadas.TotalHours;

                linha++;
            }

            worksheet.Cells.AutoFitColumns();

            return await package.GetAsByteArrayAsync();
        }
    }

}

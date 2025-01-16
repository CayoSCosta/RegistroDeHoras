namespace Testes;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        DateTime reinicio = new DateTime(2025, 1, 16, 9, 0, 0);
        DateTime pause = new DateTime(2025, 1, 15, 18, 0, 0);
        CalcularhorasDePausa(reinicio, pause);
    }

    public static void CalcularhorasDePausa(DateTime reinicio, DateTime pausa)
    {
        TimeSpan horasDePausa = reinicio - pausa;
        Console.WriteLine($"o calculo é: {horasDePausa}");
    }
}

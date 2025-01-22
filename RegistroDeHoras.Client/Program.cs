using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using RegistroDeHoras.Client.Layout;
using RegistroDeHoras.Client.Services;

namespace RegistroDeHoras.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        var url = builder.Configuration.GetSection("RegistroDeHoras.Api")["Endpoint"];
        if (string.IsNullOrEmpty(url))
            throw new ArgumentNullException(nameof(url), "The API endpoint URL cannot be null or empty.");

        builder.Services.AddHttpClient("RegistroDeHoras.Api", options =>
        {
            options.BaseAddress = new Uri(url);
            Console.WriteLine($"API endpoint URL: {url}");
        });

        builder.Services.AddMudServices();
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("RegistroDeHoras.Api"));
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        builder.Services.AddScoped<ITarefaServices, TarefaService>();
        builder.Services.AddSingleton<ThemeService>();


        await builder.Build().RunAsync();
    }
}

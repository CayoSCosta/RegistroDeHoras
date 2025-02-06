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

        builder.Services.AddHttpClient();

        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("RegistroDeHoras.Api"));

        //var url = builder.Configuration.GetSection("RegistroDeHoras.Api")["Endpoint"];
        var url = "https://localhost:7146";
        //var url = "http://localhost:5039";

        builder.Services.AddHttpClient("RegistroDeHoras.Api", options =>
        {
            options.BaseAddress = new Uri(url);
        });

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.AddMudServices();
        builder.Services.AddScoped<ITarefaServices, TarefaService>();
        builder.Services.AddSingleton<ThemeService>();


        await builder.Build().RunAsync();
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EnemPrep.Web.ApiClients;

class Program {
    static async Task Main(string[] args) {
        Console.WriteLine("Iniciando Smoke Test...");
        var services = new ServiceCollection();
        
        services.AddLogging(configure => configure.AddConsole());
        services.AddHttpClient<IVideoAulaApiClient, VideoAulaApiClient>(client => 
        {
            client.BaseAddress = new Uri("http://localhost:5001/");
        });
        
        var provider = services.BuildServiceProvider();
        var client = provider.GetRequiredService<IVideoAulaApiClient>();

        var assuntoId = Guid.Parse("1b84afde-6f20-4a11-8a23-13e9c93b9b1e");

        try {
            var items = await client.GetByAssuntoAsync(assuntoId);
            Console.WriteLine($"Sucesso na listagem! Itens: {items.Count}");
            foreach (var item in items) {
                Console.WriteLine($"- {item.Titulo}");
            }
        } catch (Exception ex) {
            Console.WriteLine($"Excecao capturada! {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}

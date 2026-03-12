using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
}

public class VideoAulaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string UrlVideo { get; set; } = string.Empty;
    public int DuracaoSegundos { get; set; }
    public Guid AssuntoId { get; set; }

    public string DuracaoFormatada => "";
}

class Program {
    static async Task Main(string[] args) {
        Console.WriteLine("Iniciando HTTP Test GET Exact Copy...");
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5001/");

        var assuntoId = "1b84afde-6f20-4a11-8a23-13e9c93b9b1e"; // Known working ID from DB Test

        try {
            var response = await client.GetFromJsonAsync<ApiResponse<List<VideoAulaViewModel>>>($"api/assuntos/{assuntoId}/videoaulas");
            Console.WriteLine($"Sucesso: {response?.Success}");
            Console.WriteLine($"Items count: {response?.Data?.Count}");
            if (response?.Data != null) {
                foreach (var item in response.Data) {
                    Console.WriteLine($"Item: {item.Titulo} ({item.Id})");
                }
            }
        } catch (Exception ex) {
            Console.WriteLine("Excecao: " + ex);
        }
    }
}

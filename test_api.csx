using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("Starting API test...");
using var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7001/");

// 1. Get a test AssuntoId
var baseId = Guid.NewGuid();

try {
    Console.WriteLine("Testing GET /api/assuntos to see if the API is up...");
    // Let's just create a test VideoAula directly with a dummy ID
    var payload = new {
        Titulo = "Test Video " + DateTime.Now.Ticks,
        UrlVideo = "http://youtube.com/watch?v=12345",
        DuracaoSegundos = 120,
        AssuntoId = Guid.NewGuid() // we'll use a random Guid
    };

    Console.WriteLine($"Sending POST to api/videoaulas with AssuntoId: {payload.AssuntoId}");
    var httpResponse = await client.PostAsJsonAsync("api/videoaulas", payload);
    
    Console.WriteLine($"Status: {httpResponse.StatusCode}");
    var json = await httpResponse.Content.ReadAsStringAsync();
    Console.WriteLine($"Response: {json}");

} catch (Exception ex) {
    Console.WriteLine($"Exception: {ex.Message}");
}

using EnemPrep.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace EnemPrep.Infrastructure.Services;

public class SupabaseStorageService : IFileStorageService
{
    private readonly HttpClient _httpClient;
    private readonly string _bucketName;
    private readonly string _supabaseUrl;

    public SupabaseStorageService(IConfiguration configuration, HttpClient httpClient)
    {
        _supabaseUrl = configuration["Supabase:Url"]?.TrimEnd('/')
            ?? throw new InvalidOperationException("Supabase:Url not configured.");
        
        var serviceKey = configuration["Supabase:ServiceRoleKey"]
            ?? throw new InvalidOperationException("Supabase:ServiceRoleKey not configured.");
            
        _bucketName = configuration["Supabase:BucketName"] ?? "enemprep-uploads";

        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri($"{_supabaseUrl}/storage/v1/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", serviceKey);
        // Supabase requires apikey header as well for REST API
        _httpClient.DefaultRequestHeaders.Add("apikey", serviceKey);
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderName, CancellationToken cancellationToken = default)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("O stream do arquivo é nulo ou vazio.");

        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = $"{folderName}/{uniqueFileName}";

        using var content = new StreamContent(fileStream);
        content.Headers.ContentType = new MediaTypeHeaderValue(GetContentType(fileName));

        // Endpoint: POST /object/{bucketName}/{wildcard}
        var response = await _httpClient.PostAsync($"object/{_bucketName}/{filePath}", content, cancellationToken);

        response.EnsureSuccessStatusCode();

        // Retorna a URL pública
        return $"{_supabaseUrl}/storage/v1/object/public/{_bucketName}/{filePath}";
    }

    public async Task DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return;

        // Extrai o caminho do arquivo (folderName/fileName) da URL completa
        // Exemplo: https://xyz.supabase.co/storage/v1/object/public/enemprep-uploads/capas/123_foto.jpg -> capas/123_foto.jpg
        var urlPrefix = $"{_supabaseUrl}/storage/v1/object/public/{_bucketName}/";
        if (filePath.StartsWith(urlPrefix))
        {
            var relativePath = filePath.Substring(urlPrefix.Length);
            
            // Endpoint: DELETE /object/{bucketName}/{wildcard}
            var response = await _httpClient.DeleteAsync($"object/{_bucketName}/{relativePath}", cancellationToken);
            // Ignoramos o erro se o arquivo já não existir
            if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.NotFound)
            {
                response.EnsureSuccessStatusCode();
            }
        }
    }

    private static string GetContentType(string fileName) => Path.GetExtension(fileName).ToLower() switch
    {
        ".jpg" or ".jpeg" => "image/jpeg",
        ".png" => "image/png",
        ".pdf" => "application/pdf",
        ".webp" => "image/webp",
        _ => "application/octet-stream"
    };
}

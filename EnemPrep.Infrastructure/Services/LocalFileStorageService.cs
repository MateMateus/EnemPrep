using EnemPrep.Application.Interfaces;
using System.IO;

namespace EnemPrep.Infrastructure.Services;

public class LocalFileStorageService : IFileStorageService
{
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
    public LocalFileStorageService(Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderName, CancellationToken cancellationToken = default)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("O stream do arquivo é nulo ou vazio.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using var destStream = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(destStream, cancellationToken);

        // Retorna o caminho relativo (ex: /materiais/arquivo.pdf)
        var baseUrl = _configuration["ApiPublicUrl"]?.TrimEnd('/') ?? "";
        if (string.IsNullOrEmpty(baseUrl)) return $"/{folderName}/{uniqueFileName}";
        return $"{baseUrl}/{folderName}/{uniqueFileName}";
    }

    public Task DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return Task.CompletedTask;

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'));
        
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}


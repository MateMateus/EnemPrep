using System.IO;

namespace EnemPrep.Application.Interfaces;

public interface IPdfProcessorService
{
    /// <summary>
    /// Processa um PDF, converte suas páginas em imagens (JPEG) e as salva em diretório público.
    /// Retorna a lista de URLs relativas das imagens geradas.
    /// </summary>
    Task<List<string>> ExtractPagesAsImagesAsync(Stream pdfStream, Guid livroId, CancellationToken cancellationToken = default);
}

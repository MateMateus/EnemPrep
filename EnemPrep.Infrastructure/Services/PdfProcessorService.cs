using Docnet.Core;
using Docnet.Core.Models;
using EnemPrep.Application.Interfaces;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace EnemPrep.Infrastructure.Services;

public class PdfProcessorService : IPdfProcessorService
{
    private readonly IFileStorageService _storageService;

    public PdfProcessorService(IFileStorageService storageService)
    {
        _storageService = storageService;
    }

    public async Task<List<string>> ExtractPagesAsImagesAsync(Stream pdfStream, Guid livroId, CancellationToken cancellationToken = default)
    {
        var urls = new List<string>();

        // Lê o stream para um byte array (Docnet.Core aceita byte array)
        using var memoryStream = new MemoryStream();
        await pdfStream.CopyToAsync(memoryStream, cancellationToken);
        var fileBytes = memoryStream.ToArray();

        // Extrai com Docnet.Core em escopo isolado
        // Renderiza com fator de escala 3x (3.0) para garantir altíssima definição no zoom
        using (var docReader = DocLib.Instance.GetDocReader(fileBytes, new PageDimensions(3.0)))
        {
            int pageCount = docReader.GetPageCount();

            for (int i = 0; i < pageCount; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var pageReader = docReader.GetPageReader(i);

                // Get image as BGRA
                var rawBytes = pageReader.GetImage();

                int width = pageReader.GetPageWidth();
                int height = pageReader.GetPageHeight();

                // Converte B-G-R-A retornado pelo Docnet para o formato interno do ImageSharp
                using var img = Image.LoadPixelData<Bgra32>(rawBytes, width, height);

                // Adiciona fundo branco para evitar a "tela preta" onde o PDF original é transparente
                img.Mutate(x => x.BackgroundColor(Color.White));

                // Salva em memória como JPEG com máxima qualidade (100%) para suportar zoom intenso
                using var jpegStream = new MemoryStream();
                await img.SaveAsJpegAsync(jpegStream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 100 }, cancellationToken);
                jpegStream.Position = 0;

                // Usa o storage service (local em dev, Azure Blob em prod)
                string fileName = $"pag_{i + 1:D3}.jpg";
                var url = await _storageService.SaveFileAsync(jpegStream, fileName, $"uploads/livros/{livroId}", cancellationToken);
                urls.Add(url);
            }
        }

        return urls;
    }
}

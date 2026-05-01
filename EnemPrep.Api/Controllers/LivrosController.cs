using EnemPrep.Api.Extensions;
using EnemPrep.Application.Common;
using EnemPrep.Application.DTOs.Livros;
using EnemPrep.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using EnemPrep.Domain.Enums;

namespace EnemPrep.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/livros")]
public class LivrosController : ControllerBase
{
    private readonly ILivroService _livroService;
    private readonly IFileStorageService _storageService;

    public LivrosController(ILivroService livroService, IFileStorageService storageService)
    {
        _livroService = livroService;
        _storageService = storageService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? busca = null,
        [FromQuery] Guid? materiaId = null,
        [FromQuery] TipoConteudo? tipo = null,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 20, 
        CancellationToken ct = default)
    {
        var result = await _livroService.GetPagedAsync(page, pageSize, busca, materiaId, tipo, ct);
        return Ok(ApiResponse<PagedResult<LivroDto>>.Ok(result.Data!));
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _livroService.GetByIdAsync(id, ct);
        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<LivroDetalhadoDto>.Ok(result.Data!));
    }

    [HttpPost]
    public async Task<IActionResult> Criar(
        [FromForm] string titulo,
        [FromForm] string? descricao,
        [FromForm] Guid materiaId,
        [FromForm] TipoConteudo tipoConteudo,
        [FromForm] string? urlCapaExistente,
        [FromForm] IFormFile? capaArquivo,
        CancellationToken ct)
    {
        string? urlCapa = urlCapaExistente;

        if (capaArquivo != null && capaArquivo.Length > 0)
        {
            using var stream = capaArquivo.OpenReadStream();
            urlCapa = await _storageService.SaveFileAsync(stream, capaArquivo.FileName, "capas", ct);
        }

        var request = new CriarLivroRequest(titulo, descricao, urlCapa, materiaId, tipoConteudo);
        var result = await _livroService.CriarAsync(request, ct);
        if (!result.Success)
            return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<LivroDto>.Ok(result.Data!));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(
        Guid id,
        [FromForm] string titulo,
        [FromForm] string? descricao,
        [FromForm] Guid materiaId,
        [FromForm] TipoConteudo tipoConteudo,
        [FromForm] string? urlCapaExistente,
        [FromForm] IFormFile? capaArquivo,
        CancellationToken ct)
    {
        string? urlCapa = urlCapaExistente;

        if (capaArquivo != null && capaArquivo.Length > 0)
        {
            using var stream = capaArquivo.OpenReadStream();
            urlCapa = await _storageService.SaveFileAsync(stream, capaArquivo.FileName, "capas", ct);
        }

        var request = new CriarLivroRequest(titulo, descricao, urlCapa, materiaId, tipoConteudo);
        var result = await _livroService.AtualizarAsync(id, request, ct);
        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id, CancellationToken ct)
    {
        var result = await _livroService.DeletarAsync(id, ct);
        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpPost("{livroId:guid}/paginas")]
    public async Task<IActionResult> AdicionarPaginas(Guid livroId, [FromBody] AdicionarPaginasRequest request, CancellationToken ct)
    {
        var result = await _livroService.AdicionarPaginasAsync(livroId, request, ct);
        if (!result.Success)
            return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

        return Ok(ApiResponse<int>.Ok(result.Data));
    }

    [EnableRateLimiting("upload")]
    [HttpPost("{livroId:guid}/paginas/upload")]
    public async Task<IActionResult> UploadPdf(Guid livroId, IFormFile file, [FromServices] IPdfProcessorService pdfService, CancellationToken ct)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ApiResponse.Fail("Selecione um arquivo PDF válido."));

        if (!file.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase) && !file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            return BadRequest(ApiResponse.Fail("O arquivo deve ser um PDF."));

        try
        {
            using var stream = file.OpenReadStream();
            var urls = await pdfService.ExtractPagesAsImagesAsync(stream, livroId, ct);
            
            if (urls.Count == 0)
                return BadRequest(ApiResponse.Fail("Nenhuma página pôde ser extraída do PDF."));

            var result = await _livroService.AdicionarPaginasAsync(livroId, new AdicionarPaginasRequest(urls), ct);
            
            if (!result.Success)
                return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

            return Ok(ApiResponse<int>.Ok(result.Data));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse.Fail($"Erro ao processar PDF: {ex.Message}"));
        }
    }

    [HttpDelete("{livroId:guid}/paginas/{paginaId:guid}")]
    public async Task<IActionResult> RemoverPagina(Guid livroId, Guid paginaId, CancellationToken ct)
    {
        var result = await _livroService.RemoverPaginaAsync(livroId, paginaId, ct);
        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpPost("{livroId:guid}/temas")]
    public async Task<IActionResult> CriarTema(Guid livroId, [FromBody] CriarTemaRequest request, CancellationToken ct)
    {
        var result = await _livroService.CriarTemaAsync(livroId, request, ct);
        if (!result.Success)
            return BadRequest(ApiResponse.Fail(result.ErrorMessage!));

        return Created(string.Empty, ApiResponse<LivroTemaDto>.Ok(result.Data!));
    }

    [HttpPut("{livroId:guid}/temas/{temaId:guid}")]
    public async Task<IActionResult> AtualizarTema(Guid livroId, Guid temaId, [FromBody] CriarTemaRequest request, CancellationToken ct)
    {
        var result = await _livroService.AtualizarTemaAsync(livroId, temaId, request, ct);
        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }

    [HttpDelete("{livroId:guid}/temas/{temaId:guid}")]
    public async Task<IActionResult> DeletarTema(Guid livroId, Guid temaId, CancellationToken ct)
    {
        var result = await _livroService.DeletarTemaAsync(livroId, temaId, ct);
        if (!result.Success)
            return NotFound(ApiResponse.Fail(result.ErrorMessage!));

        return NoContent();
    }
}

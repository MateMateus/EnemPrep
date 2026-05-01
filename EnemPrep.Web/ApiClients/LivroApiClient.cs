using System.Net.Http.Json;
using EnemPrep.Web.Models.Shared;
using EnemPrep.Web.Enums;

namespace EnemPrep.Web.ApiClients;

public interface ILivroApiClient
{
    Task<List<LivroListViewModel>> GetAllAsync(string? busca = null, Guid? materiaId = null, TipoConteudo? tipo = null, CancellationToken ct = default);
    Task<LivroDetalhesViewModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<LivroListViewModel?> CriarAsync(string titulo, string? descricao, string? urlCapa, Guid materiaId, TipoConteudo tipoConteudo, IFormFile? capaArquivo, CancellationToken ct = default);
    Task<bool> AtualizarAsync(Guid id, string titulo, string? descricao, string? urlCapa, Guid materiaId, TipoConteudo tipoConteudo, IFormFile? capaArquivo, CancellationToken ct = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken ct = default);
    Task<bool> AdicionarPaginasAsync(Guid livroId, List<string> urls, CancellationToken ct = default);
    Task<bool> RemoverPaginaAsync(Guid livroId, Guid paginaId, CancellationToken ct = default);
    Task<bool> UploadPdfAsync(Guid livroId, Stream pdfStream, string fileName, CancellationToken ct = default);
    Task<bool> CriarTemaAsync(Guid livroId, string nome, int paginaInicial, int paginaFinal, CancellationToken ct = default);
    Task<bool> AtualizarTemaAsync(Guid livroId, Guid temaId, string nome, int paginaInicial, int paginaFinal, CancellationToken ct = default);
    Task<bool> DeletarTemaAsync(Guid livroId, Guid temaId, CancellationToken ct = default);
}

public class LivroApiClient(HttpClient http) : ILivroApiClient
{
    public async Task<List<LivroListViewModel>> GetAllAsync(string? busca = null, Guid? materiaId = null, TipoConteudo? tipo = null, CancellationToken ct = default)
    {
        var query = new List<string> { "pageSize=100" };
        if (!string.IsNullOrEmpty(busca)) query.Add($"busca={Uri.EscapeDataString(busca)}");
        if (materiaId.HasValue) query.Add($"materiaId={materiaId.Value}");
        if (tipo.HasValue) query.Add($"tipo={(int)tipo.Value}");

        var queryString = string.Join("&", query);
        var r = await http.GetFromJsonAsync<ApiResponse<PagedApiResponse<LivroListViewModel>>>($"api/livros?{queryString}", ct);
        return r?.Data?.Items ?? [];
    }

    public async Task<LivroDetalhesViewModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var r = await http.GetFromJsonAsync<ApiResponse<LivroDetalhesViewModel>>($"api/livros/{id}", ct);
        return r?.Data;
    }

    public async Task<LivroListViewModel?> CriarAsync(string titulo, string? descricao, string? urlCapa, Guid materiaId, TipoConteudo tipoConteudo, IFormFile? capaArquivo, CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(titulo), "titulo");
        if (descricao != null) content.Add(new StringContent(descricao), "descricao");
        content.Add(new StringContent(materiaId.ToString()), "materiaId");
        content.Add(new StringContent(((int)tipoConteudo).ToString()), "tipoConteudo");
        if (urlCapa != null) content.Add(new StringContent(urlCapa), "urlCapaExistente");

        if (capaArquivo != null && capaArquivo.Length > 0)
        {
            var streamContent = new StreamContent(capaArquivo.OpenReadStream());
            content.Add(streamContent, "capaArquivo", capaArquivo.FileName);
        }

        var resp = await http.PostAsync("api/livros", content, ct);
        if (!resp.IsSuccessStatusCode) return null;
        var r = await resp.Content.ReadFromJsonAsync<ApiResponse<LivroListViewModel>>(ct);
        return r?.Data;
    }

    public async Task<bool> AtualizarAsync(Guid id, string titulo, string? descricao, string? urlCapa, Guid materiaId, TipoConteudo tipoConteudo, IFormFile? capaArquivo, CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(titulo), "titulo");
        if (descricao != null) content.Add(new StringContent(descricao), "descricao");
        content.Add(new StringContent(materiaId.ToString()), "materiaId");
        content.Add(new StringContent(((int)tipoConteudo).ToString()), "tipoConteudo");
        if (urlCapa != null) content.Add(new StringContent(urlCapa), "urlCapaExistente");

        if (capaArquivo != null && capaArquivo.Length > 0)
        {
            var streamContent = new StreamContent(capaArquivo.OpenReadStream());
            content.Add(streamContent, "capaArquivo", capaArquivo.FileName);
        }

        var resp = await http.PutAsync($"api/livros/{id}", content, ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"api/livros/{id}", ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> AdicionarPaginasAsync(Guid livroId, List<string> urls, CancellationToken ct = default)
    {
        var resp = await http.PostAsJsonAsync($"api/livros/{livroId}/paginas", new { UrlsImagens = urls }, ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> RemoverPaginaAsync(Guid livroId, Guid paginaId, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"api/livros/{livroId}/paginas/{paginaId}", ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> UploadPdfAsync(Guid livroId, Stream pdfStream, string fileName, CancellationToken ct = default)
    {
        using var content = new MultipartFormDataContent();
        using var fileContent = new StreamContent(pdfStream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
        content.Add(fileContent, "file", fileName);

        var resp = await http.PostAsync($"api/livros/{livroId}/paginas/upload", content, ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> CriarTemaAsync(Guid livroId, string nome, int paginaInicial, int paginaFinal, CancellationToken ct = default)
    {
        var resp = await http.PostAsJsonAsync($"api/livros/{livroId}/temas", new { Nome = nome, PaginaInicial = paginaInicial, PaginaFinal = paginaFinal }, ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> AtualizarTemaAsync(Guid livroId, Guid temaId, string nome, int paginaInicial, int paginaFinal, CancellationToken ct = default)
    {
        var resp = await http.PutAsJsonAsync($"api/livros/{livroId}/temas/{temaId}", new { Nome = nome, PaginaInicial = paginaInicial, PaginaFinal = paginaFinal }, ct);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> DeletarTemaAsync(Guid livroId, Guid temaId, CancellationToken ct = default)
    {
        var resp = await http.DeleteAsync($"api/livros/{livroId}/temas/{temaId}", ct);
        return resp.IsSuccessStatusCode;
    }
}

public class PagedApiResponse<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
}

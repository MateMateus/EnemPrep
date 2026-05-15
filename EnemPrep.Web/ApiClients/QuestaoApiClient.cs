using System.Net.Http.Json;
using EnemPrep.Web.Models.Shared;

namespace EnemPrep.Web.ApiClients;

public interface IQuestaoApiClient
{
    Task<List<QuestaoViewModel>> GetByAssuntoAsync(Guid assuntoId, CancellationToken ct = default);
    Task<List<QuestaoViewModel>> GetByTemaAsync(Guid temaId, CancellationToken ct = default);
    Task<QuestaoViewModel?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<QuestaoViewModel?> CriarAsync(
        string enunciado, int dificuldade, Guid assuntoId, string? explicacao, string? videoExplicacaoUrl,
        IEnumerable<(string Texto, bool IsCorreta)> alternativas, Microsoft.AspNetCore.Http.IFormFile? imagemArquivo,
        Guid? livroId = null, Guid? livroTemaId = null,
        CancellationToken ct = default);
    Task<QuestaoViewModel?> AtualizarAsync(
        Guid id, string enunciado, int dificuldade, string? explicacao, string? videoExplicacaoUrl, string? imagemUrlExistente,
        IEnumerable<(string Texto, bool IsCorreta)> alternativas, Microsoft.AspNetCore.Http.IFormFile? imagemArquivo,
        Guid? livroId = null, Guid? livroTemaId = null,
        CancellationToken ct = default);
    Task<ResultadoQuestaoViewModel?> ResponderAsync(Guid usuarioId, Guid questaoId, Guid alternativaId, int tempoGasto, CancellationToken ct = default);
    Task<List<TentativaQuestaoViewModel>> GetHistoricoAsync(Guid usuarioId, CancellationToken ct = default);
    Task<bool> DeletarAsync(Guid id, CancellationToken ct = default);
}

public class QuestaoApiClient(HttpClient http) : IQuestaoApiClient
{
    public async Task<List<QuestaoViewModel>> GetByAssuntoAsync(Guid assuntoId, CancellationToken ct = default)
    {
        var response = await http.GetFromJsonAsync<ApiResponse<PagedData<QuestaoViewModel>>>(
            $"api/assuntos/{assuntoId}/questoes?Page=1&PageSize=50", ct);
        return response?.Data?.Items ?? [];
    }

    public async Task<List<QuestaoViewModel>> GetByTemaAsync(Guid temaId, CancellationToken ct = default)
    {
        var response = await http.GetFromJsonAsync<ApiResponse<PagedData<QuestaoViewModel>>>(
            $"api/temas/{temaId}/questoes?Page=1&PageSize=50", ct);
        return response?.Data?.Items ?? [];
    }


    public async Task<QuestaoViewModel?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        try
        {
            var response = await http.GetFromJsonAsync<ApiResponse<QuestaoViewModel>>($"api/questoes/{id}", ct);
            return response?.Data;
        }
        catch { return null; }
    }

    public async Task<QuestaoViewModel?> CriarAsync(
        string enunciado, int dificuldade, Guid assuntoId, string? explicacao, string? videoExplicacaoUrl,
        IEnumerable<(string Texto, bool IsCorreta)> alternativas, Microsoft.AspNetCore.Http.IFormFile? imagemArquivo,
        Guid? livroId = null, Guid? livroTemaId = null,
        CancellationToken ct = default)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(enunciado), "Enunciado");
            content.Add(new StringContent(dificuldade.ToString()), "Dificuldade");
            content.Add(new StringContent(assuntoId.ToString()), "AssuntoId");
            
            if (!string.IsNullOrWhiteSpace(explicacao))
                content.Add(new StringContent(explicacao), "Explicacao");
                
            if (!string.IsNullOrWhiteSpace(videoExplicacaoUrl))
                content.Add(new StringContent(videoExplicacaoUrl), "VideoExplicacaoUrl");

            if (livroId.HasValue)
                content.Add(new StringContent(livroId.Value.ToString()), "LivroId");

            if (livroTemaId.HasValue)
                content.Add(new StringContent(livroTemaId.Value.ToString()), "LivroTemaId");

            var altArray = alternativas.ToArray();
            for (int i = 0; i < altArray.Length; i++)
            {
                content.Add(new StringContent(altArray[i].Texto), $"Alternativas[{i}].Texto");
                content.Add(new StringContent(altArray[i].IsCorreta.ToString().ToLower()), $"Alternativas[{i}].Correta");
            }

            if (imagemArquivo != null && imagemArquivo.Length > 0)
            {
                var streamContent = new StreamContent(imagemArquivo.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imagemArquivo.ContentType);
                content.Add(streamContent, "ImagemArquivo", imagemArquivo.FileName);
            }

            var httpResponse = await http.PostAsync("api/questoes", content, ct);
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorBody = await httpResponse.Content.ReadAsStringAsync(ct);
                throw new ApplicationException($"API Error ({httpResponse.StatusCode}): {errorBody}");
            }
            var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<QuestaoViewModel>>(ct);
            return response?.Data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[QuestaoApiClient] EXCEÇÃO ao criar questão: {ex.Message}");
            return null;
        }
    }

    public async Task<QuestaoViewModel?> AtualizarAsync(
        Guid id, string enunciado, int dificuldade, string? explicacao, string? videoExplicacaoUrl, string? imagemUrlExistente,
        IEnumerable<(string Texto, bool IsCorreta)> alternativas, Microsoft.AspNetCore.Http.IFormFile? imagemArquivo,
        Guid? livroId = null, Guid? livroTemaId = null,
        CancellationToken ct = default)
    {
        try
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(enunciado), "Enunciado");
            content.Add(new StringContent(dificuldade.ToString()), "Dificuldade");
            
            if (!string.IsNullOrWhiteSpace(explicacao))
                content.Add(new StringContent(explicacao), "Explicacao");
                
            if (!string.IsNullOrWhiteSpace(videoExplicacaoUrl))
                content.Add(new StringContent(videoExplicacaoUrl), "VideoExplicacaoUrl");

            if (!string.IsNullOrWhiteSpace(imagemUrlExistente))
                content.Add(new StringContent(imagemUrlExistente), "ImagemUrl");

            if (livroId.HasValue)
                content.Add(new StringContent(livroId.Value.ToString()), "LivroId");

            if (livroTemaId.HasValue)
                content.Add(new StringContent(livroTemaId.Value.ToString()), "LivroTemaId");

            var altArray = alternativas.ToArray();
            for (int i = 0; i < altArray.Length; i++)
            {
                content.Add(new StringContent(altArray[i].Texto), $"Alternativas[{i}].Texto");
                content.Add(new StringContent(altArray[i].IsCorreta.ToString().ToLower()), $"Alternativas[{i}].Correta");
            }

            if (imagemArquivo != null && imagemArquivo.Length > 0)
            {
                var streamContent = new StreamContent(imagemArquivo.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(imagemArquivo.ContentType);
                content.Add(streamContent, "ImagemArquivo", imagemArquivo.FileName);
            }

            var httpResponse = await http.PutAsync($"api/questoes/{id}", content, ct);
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorBody = await httpResponse.Content.ReadAsStringAsync(ct);
                throw new ApplicationException($"API Error ({httpResponse.StatusCode}): {errorBody}");
            }
            var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<QuestaoViewModel>>(ct);
            return response?.Data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[QuestaoApiClient] EXCEÇÃO ao atualizar questão: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeletarAsync(Guid id, CancellationToken ct = default)
    {
        var httpResponse = await http.DeleteAsync($"api/questoes/{id}", ct);
        return httpResponse.IsSuccessStatusCode;
    }

    public async Task<ResultadoQuestaoViewModel?> ResponderAsync(Guid usuarioId, Guid questaoId, Guid alternativaId, int tempoGasto, CancellationToken ct = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "api/questoes/responder")
        {
            Content = JsonContent.Create(new { QuestaoId = questaoId, AlternativaSelecionadaId = alternativaId, TempoGastoSegundos = tempoGasto })
        };
        request.Headers.Add("X-Usuario-Id", usuarioId.ToString());

        var httpResponse = await http.SendAsync(request, ct);
        if (!httpResponse.IsSuccessStatusCode) return null;

        var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<ResultadoQuestaoViewModel>>(ct);
        return response?.Data;
    }

    public async Task<List<TentativaQuestaoViewModel>> GetHistoricoAsync(Guid usuarioId, CancellationToken ct = default)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "api/questoes/tentativas");
        request.Headers.Add("X-Usuario-Id", usuarioId.ToString());

        try
        {
            var httpResponse = await http.SendAsync(request, ct);
            if (!httpResponse.IsSuccessStatusCode) return [];

            var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<TentativaQuestaoViewModel>>>(ct);
            return response?.Data?.ToList() ?? [];
        }
        catch { return []; }
    }
}



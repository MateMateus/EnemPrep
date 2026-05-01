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
        IEnumerable<(string Texto, bool IsCorreta)> alternativas,
        Guid? livroId = null, Guid? livroTemaId = null,
        CancellationToken ct = default);
    Task<QuestaoViewModel?> AtualizarAsync(
        Guid id, string enunciado, int dificuldade, string? explicacao, string? videoExplicacaoUrl,
        IEnumerable<(string Texto, bool IsCorreta)> alternativas,
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
        IEnumerable<(string Texto, bool IsCorreta)> alternativas,
        Guid? livroId = null, Guid? livroTemaId = null,
        CancellationToken ct = default)
    {
        try
        {
            var payload = new
            {
                Enunciado = enunciado,
                Dificuldade = dificuldade,
                AssuntoId = assuntoId,
                Explicacao = string.IsNullOrWhiteSpace(explicacao) ? (string?)null : explicacao,
                VideoExplicacaoUrl = string.IsNullOrWhiteSpace(videoExplicacaoUrl) ? (string?)null : videoExplicacaoUrl,
                Alternativas = alternativas.Select(a => new { Texto = a.Texto, Correta = a.IsCorreta }),
                LivroId = livroId,
                LivroTemaId = livroTemaId
            };

            var httpResponse = await http.PostAsJsonAsync("api/questoes", payload, ct);
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorBody = await httpResponse.Content.ReadAsStringAsync(ct);
                Console.WriteLine($"[QuestaoApiClient] ERRO {httpResponse.StatusCode}: {errorBody}");
                return null;
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
        Guid id, string enunciado, int dificuldade, string? explicacao, string? videoExplicacaoUrl,
        IEnumerable<(string Texto, bool IsCorreta)> alternativas,
        Guid? livroId = null, Guid? livroTemaId = null,
        CancellationToken ct = default)
    {
        try
        {
            var payload = new
            {
                Enunciado = enunciado,
                Dificuldade = dificuldade,
                Explicacao = string.IsNullOrWhiteSpace(explicacao) ? (string?)null : explicacao,
                VideoExplicacaoUrl = string.IsNullOrWhiteSpace(videoExplicacaoUrl) ? (string?)null : videoExplicacaoUrl,
                Alternativas = alternativas.Select(a => new { Texto = a.Texto, Correta = a.IsCorreta }),
                LivroId = livroId,
                LivroTemaId = livroTemaId
            };

            var httpResponse = await http.PutAsJsonAsync($"api/questoes/{id}", payload, ct);
            if (!httpResponse.IsSuccessStatusCode)
            {
                var errorBody = await httpResponse.Content.ReadAsStringAsync(ct);
                Console.WriteLine($"[QuestaoApiClient] ERRO {httpResponse.StatusCode}: {errorBody}");
                return null;
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

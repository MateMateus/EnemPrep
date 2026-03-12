namespace EnemPrep.Web.ApiClients;

/// <summary>
/// Wrapper que espelha o formato de resposta da API sem depender de EnemPrep.Api.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Para respostas paginadas.
/// </summary>
public class PagedData<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

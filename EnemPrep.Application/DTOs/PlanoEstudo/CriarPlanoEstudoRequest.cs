namespace EnemPrep.Application.DTOs.PlanoEstudo;

public record CriarPlanoEstudoRequest(
    string Titulo,
    DateTime DataInicio,
    DateTime DataFim,
    IReadOnlyList<CriarPlanoEstudoItemRequest> Itens
);

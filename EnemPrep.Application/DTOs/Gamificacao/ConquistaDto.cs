namespace EnemPrep.Application.DTOs.Gamificacao;

public record ConquistaDto(
    Guid Id,
    string Titulo,
    string Descricao,
    string Icone,
    int PontosZ,
    bool Desbloqueada,
    DateTime? DataObtencao);

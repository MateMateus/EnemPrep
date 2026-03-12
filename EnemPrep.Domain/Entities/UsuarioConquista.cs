namespace EnemPrep.Domain.Entities;

public class UsuarioConquista : Entity
{
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;
    
    public Guid ConquistaId { get; private set; }
    public Conquista Conquista { get; private set; } = null!;

    public DateTime DataObtencao { get; private set; }

    protected UsuarioConquista() { }

    public UsuarioConquista(Guid usuarioId, Guid conquistaId)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("Usuário inválido.", nameof(usuarioId));
        if (conquistaId == Guid.Empty) throw new ArgumentException("Conquista inválida.", nameof(conquistaId));

        UsuarioId = usuarioId;
        ConquistaId = conquistaId;
        DataObtencao = DateTime.UtcNow;
    }
}

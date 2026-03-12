namespace EnemPrep.Domain.Entities;

public class StreakUsuario : Entity
{
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public int DiasConsecutivos { get; private set; }
    public int MaiorStreak { get; private set; }
    public DateTime UltimaAtividade { get; private set; }

    protected StreakUsuario() { }

    public StreakUsuario(Guid usuarioId)
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("Usuário inválido.", nameof(usuarioId));

        UsuarioId = usuarioId;
        DiasConsecutivos = 0;
        MaiorStreak = 0;
        UltimaAtividade = DateTime.UtcNow;
    }

    public void RegistrarAtividade()
    {
        var hoje = DateTime.UtcNow.Date;
        var ultima = UltimaAtividade.Date;

        if (hoje == ultima) return;

        if (hoje == ultima.AddDays(1))
        {
            DiasConsecutivos++;
        }
        else
        {
            DiasConsecutivos = 1;
        }

        if (DiasConsecutivos > MaiorStreak)
        {
            MaiorStreak = DiasConsecutivos;
        }

        UltimaAtividade = DateTime.UtcNow;
        AtualizarData();
    }
}

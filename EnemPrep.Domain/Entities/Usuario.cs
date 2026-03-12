namespace EnemPrep.Domain.Entities;

public class Usuario : Entity
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    
    public Guid PerfilUsuarioId { get; private set; }
    public PerfilUsuario PerfilUsuario { get; private set; } = null!;

    private readonly List<TentativaQuestao> _tentativas;
    public IReadOnlyCollection<TentativaQuestao> Tentativas => _tentativas.AsReadOnly();

    private readonly List<PlanoEstudo> _planosEstudo;
    public IReadOnlyCollection<PlanoEstudo> PlanosEstudo => _planosEstudo.AsReadOnly();

    private readonly List<UsuarioConquista> _conquistas;
    public IReadOnlyCollection<UsuarioConquista> Conquistas => _conquistas.AsReadOnly();

    public StreakUsuario? Streak { get; private set; }

    protected Usuario() 
    { 
        _tentativas = new List<TentativaQuestao>();
        _planosEstudo = new List<PlanoEstudo>();
        _conquistas = new List<UsuarioConquista>();
        Nome = string.Empty;
        Email = string.Empty;
        SenhaHash = string.Empty;
    }

    public Usuario(string nome, string email, string senhaHash, Guid perfilUsuarioId) : this()
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.", nameof(nome));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório.", nameof(email));
        if (string.IsNullOrWhiteSpace(senhaHash)) throw new ArgumentException("SenhaHash é obrigatória.", nameof(senhaHash));
        if (perfilUsuarioId == Guid.Empty) throw new ArgumentException("Perfil é obrigatório.", nameof(perfilUsuarioId));

        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        PerfilUsuarioId = perfilUsuarioId;
    }

    public void AtualizarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new ArgumentException("Nome não pode ser vazio.", nameof(novoNome));
        Nome = novoNome;
    }

    public void AtualizarEmail(string novoEmail)
    {
        if (string.IsNullOrWhiteSpace(novoEmail))
            throw new ArgumentException("Email não pode ser vazio.", nameof(novoEmail));
        Email = novoEmail;
    }

    public void AtualizarSenha(string novoSenhaHash)
    {
        if (string.IsNullOrWhiteSpace(novoSenhaHash))
            throw new ArgumentException("Hash de senha não pode ser vazio.", nameof(novoSenhaHash));
        SenhaHash = novoSenhaHash;
    }
}

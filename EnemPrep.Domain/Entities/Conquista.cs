namespace EnemPrep.Domain.Entities;

public class Conquista : Entity
{
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public string Icone { get; private set; }
    public int PontosZ { get; private set; } // Moeda/XP do jogo

    private readonly List<UsuarioConquista> _usuariosConquista;
    public IReadOnlyCollection<UsuarioConquista> UsuariosConquista => _usuariosConquista.AsReadOnly();

    protected Conquista() 
    { 
        _usuariosConquista = new List<UsuarioConquista>();
        Titulo = string.Empty;
        Descricao = string.Empty;
        Icone = string.Empty;
    }

    public Conquista(string titulo, string descricao, string icone, int pontosZ) : this()
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título inválido.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(descricao)) throw new ArgumentException("Descrição inválida.", nameof(descricao));
        
        Titulo = titulo;
        Descricao = descricao;
        Icone = icone;
        PontosZ = pontosZ;
    }
}

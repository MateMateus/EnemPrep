using EnemPrep.Domain.Enums;

namespace EnemPrep.Domain.Entities;

public class PerfilUsuario : Entity
{
    public TipoPerfil Tipo { get; private set; }
    public string NomeApresentacao { get; private set; }

    private readonly List<Usuario> _usuarios;
    public IReadOnlyCollection<Usuario> Usuarios => _usuarios.AsReadOnly();

    protected PerfilUsuario() 
    { 
        _usuarios = new List<Usuario>();
        NomeApresentacao = string.Empty;
    }

    public PerfilUsuario(TipoPerfil tipo, string nomeApresentacao) : this()
    {
        if (string.IsNullOrWhiteSpace(nomeApresentacao)) 
            throw new ArgumentException("NomeApresentacao é obrigatório.", nameof(nomeApresentacao));
        
        Tipo = tipo;
        NomeApresentacao = nomeApresentacao;
    }
}

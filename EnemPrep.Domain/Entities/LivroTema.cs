namespace EnemPrep.Domain.Entities;

public class LivroTema : Entity
{
    public Guid LivroId { get; private set; }
    public string Nome { get; private set; }
    public int PaginaInicial { get; private set; }
    public int PaginaFinal { get; private set; }

    public Livro Livro { get; private set; } = null!;

    public IReadOnlyCollection<Questao> Questoes { get; private set; } = new List<Questao>();

    protected LivroTema() { Nome = string.Empty; }

    public LivroTema(Guid livroId, string nome, int paginaInicial, int paginaFinal)
    {
        LivroId = livroId;
        Nome = nome;
        PaginaInicial = paginaInicial;
        PaginaFinal = paginaFinal;
    }

    public void Atualizar(string nome, int paginaInicial, int paginaFinal)
    {
        Nome = nome;
        PaginaInicial = paginaInicial;
        PaginaFinal = paginaFinal;
    }
}

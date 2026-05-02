namespace EnemPrep.Domain.Entities;

public class LivroPagina : Entity
{
    public Guid LivroId { get; private set; }
    public int NumeroProprio { get; private set; }
    public string UrlImagem { get; private set; }

    public Livro Livro { get; private set; } = null!;

    protected LivroPagina() { UrlImagem = string.Empty; }

    public LivroPagina(Guid livroId, int numeroProprio, string urlImagem)
    {
        LivroId = livroId;
        NumeroProprio = numeroProprio;
        UrlImagem = urlImagem;
    }
}

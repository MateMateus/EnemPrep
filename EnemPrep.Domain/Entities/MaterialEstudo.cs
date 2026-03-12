using EnemPrep.Domain.Enums;

namespace EnemPrep.Domain.Entities;

public class MaterialEstudo : Entity
{
    public string Titulo { get; private set; }
    public TipoMaterialEstudo Tipo { get; private set; }
    public string UrlArquivo { get; private set; }

    public Guid AssuntoId { get; private set; }
    public Assunto Assunto { get; private set; } = null!;

    protected MaterialEstudo() 
    { 
        Titulo = string.Empty;
        UrlArquivo = string.Empty;
    }

    public MaterialEstudo(string titulo, TipoMaterialEstudo tipo, string urlArquivo, Guid assuntoId)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentException("Título é obrigatório.", nameof(titulo));
        if (string.IsNullOrWhiteSpace(urlArquivo)) throw new ArgumentException("URL do arquivo é obrigatória.", nameof(urlArquivo));
        if (assuntoId == Guid.Empty) throw new ArgumentException("Assunto é obrigatório.", nameof(assuntoId));

        Titulo = titulo;
        Tipo = tipo;
        UrlArquivo = urlArquivo;
        AssuntoId = assuntoId;
    }
}

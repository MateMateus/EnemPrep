namespace EnemPrep.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime? DataAtualizacao { get; protected set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
    }

    public void AtualizarData()
    {
        DataAtualizacao = DateTime.UtcNow;
    }
}

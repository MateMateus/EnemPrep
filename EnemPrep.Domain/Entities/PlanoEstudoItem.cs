using EnemPrep.Domain.Enums;

namespace EnemPrep.Domain.Entities;

public class PlanoEstudoItem : Entity
{
    public Guid PlanoEstudoId { get; private set; }
    public PlanoEstudo PlanoEstudo { get; private set; } = null!;

    public Guid AssuntoId { get; private set; }
    public Assunto Assunto { get; private set; } = null!;

    public DateTime DataPrevista { get; private set; }
    public StatusPlanoItem Status { get; private set; }

    protected PlanoEstudoItem() { }

    public PlanoEstudoItem(Guid planoEstudoId, Guid assuntoId, DateTime dataPrevista)
    {
        if (planoEstudoId == Guid.Empty) throw new ArgumentException("Plano de Estudo é obrigatório.", nameof(planoEstudoId));
        if (assuntoId == Guid.Empty) throw new ArgumentException("Assunto é obrigatório.", nameof(assuntoId));

        PlanoEstudoId = planoEstudoId;
        AssuntoId = assuntoId;
        DataPrevista = dataPrevista;
        Status = StatusPlanoItem.Pendente;
    }

    public void AtualizarStatus(StatusPlanoItem novoStatus)
    {
        Status = novoStatus;
        AtualizarData();
    }
}

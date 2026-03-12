namespace EnemPrep.Domain.Entities;

public class TentativaSimulado : Entity
{
    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!;

    public Guid SimuladoId { get; private set; }
    public Simulado Simulado { get; private set; } = null!;

    public DateTime DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public int? NotaTotalBruta { get; private set; } // Số total de acertos
    
    // Status de tempo excedido? Não vamos colocar agora. Mantemos o mais simples.

    private readonly List<RespostaSimulado> _respostas;
    public IReadOnlyCollection<RespostaSimulado> Respostas => _respostas.AsReadOnly();

    protected TentativaSimulado() 
    { 
        _respostas = new List<RespostaSimulado>();
    }

    public TentativaSimulado(Guid usuarioId, Guid simuladoId) : this()
    {
        if (usuarioId == Guid.Empty) throw new ArgumentException("Usuário inválido.", nameof(usuarioId));
        if (simuladoId == Guid.Empty) throw new ArgumentException("Simulado inválido.", nameof(simuladoId));

        UsuarioId = usuarioId;
        SimuladoId = simuladoId;
        DataInicio = DateTime.UtcNow;
    }

    public void Finalizar(List<RespostaSimulado> respostasSalvas)
    {
        if (DataFim.HasValue) throw new InvalidOperationException("Tentativa já finalizada.");

        DataFim = DateTime.UtcNow;
        
        int acertos = 0;
        foreach(var resp in respostasSalvas)
        {
            if (resp.TentativaSimuladoId != Id)
                throw new ArgumentException("Resposta pertence a outra tentativa.");
            
            if (resp.Correta) acertos++;
            
            _respostas.Add(resp);
        }
        
        NotaTotalBruta = acertos;
    }
}

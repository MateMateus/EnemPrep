using EnemPrep.Web.Enums;

namespace EnemPrep.Web.Models.PlanosEstudo;

public class PlanoEstudoViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
    public List<PlanoEstudoItemViewModel> Itens { get; set; } = new();

    public int TotalItens => Itens.Count;
    public int ItensConcluidos => Itens.Count(i => i.Status == StatusPlanoItem.Concluido);
    public int ProgressoPercentual => TotalItens == 0 ? 0 : (int)((double)ItensConcluidos / TotalItens * 100);

    public List<AgendaDiaViewModel> Agenda { get; set; } = new();
}

public class AgendaDiaViewModel
{
    public DateTime Data { get; set; }
    public List<PlanoEstudoItemViewModel> Itens { get; set; } = new();
    public bool EhHoje => Data.Date == DateTime.Today;
    public bool PerdeuPrazo => Data.Date < DateTime.Today && Itens.Any(i => i.Status == StatusPlanoItem.Pendente);
}

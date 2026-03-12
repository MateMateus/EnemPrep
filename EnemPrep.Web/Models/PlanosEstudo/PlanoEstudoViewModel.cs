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
}

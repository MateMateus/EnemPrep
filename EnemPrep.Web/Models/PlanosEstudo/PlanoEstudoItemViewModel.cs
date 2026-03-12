using EnemPrep.Web.Enums;

namespace EnemPrep.Web.Models.PlanosEstudo;

public class PlanoEstudoItemViewModel
{
    public Guid Id { get; set; }
    public Guid AssuntoId { get; set; }
    public string NomeAssunto { get; set; } = string.Empty;
    public string NomeMateria { get; set; } = string.Empty;
    public DateTime DataPrevista { get; set; }
    public StatusPlanoItem Status { get; set; }

    public bool EstaAtrasado => Status == StatusPlanoItem.Pendente && DataPrevista.Date < DateTime.Today;
    public bool EhHoje => DataPrevista.Date == DateTime.Today;
}

namespace EnemPrep.Web.Models.Shared;

public class EstatisticasMateriaViewModel
{
    public Guid MateriaId { get; set; }
    public string NomeMateria { get; set; } = string.Empty;
    public int TotalRespondidas { get; set; }
    public int TotalAcertos { get; set; }
    public double PercentualAcerto { get; set; }
    public int TotalErros => TotalRespondidas - TotalAcertos;
}

using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Areas.Admin.ViewModels.Questoes;

public class AlternativaViewModel
{
    [StringLength(500, MinimumLength = 1)]
    public string? Texto { get; set; }

    public bool IsCorreta { get; set; }
}

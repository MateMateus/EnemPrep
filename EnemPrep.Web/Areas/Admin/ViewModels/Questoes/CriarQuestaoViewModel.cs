using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Areas.Admin.ViewModels.Questoes;

public class CriarQuestaoViewModel
{
    [Required]
    public Guid AssuntoId { get; set; }

    public string AssuntoNome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enunciado é obrigatório")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Enunciado deve ter entre 10 e 2000 caracteres")]
    public string Enunciado { get; set; } = string.Empty;

    [Required(ErrorMessage = "Dificuldade é obrigatória")]
    public string Dificuldade { get; set; } = "Medio"; // Facil | Medio | Dificil

    [StringLength(1000)]
    public string? Explicacao { get; set; }

    [Required]
    [MinLength(2, ErrorMessage = "Questão precisa ter ao menos 2 alternativas")]
    public List<AlternativaViewModel> Alternativas { get; set; } = [
        new(), new(), new(), new(), new()
    ];
}

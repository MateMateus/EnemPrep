using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Models.PlanosEstudo;

public class CriarPlanoEstudoViewModel
{
    [Required(ErrorMessage = "O título do plano é obrigatório.")]
    [Display(Name = "Título do Plano")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de início é obrigatória.")]
    [DataType(DataType.Date)]
    [Display(Name = "Data de Início")]
    public DateTime DataInicio { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "A data de fim é obrigatória.")]
    [DataType(DataType.Date)]
    [Display(Name = "Data Fim")]
    public DateTime DataFim { get; set; } = DateTime.Today.AddDays(7);

    // List of available subjects (Assuntos) to be selected. Will be populated by the controller.
    public List<MateriasComAssuntosViewModel> MateriasDisponiveis { get; set; } = new();

    // The selected items bound to the form submission.
    public List<CriarPlanoEstudoItemViewModel> ItensSelecionados { get; set; } = new();
}

public class MateriasComAssuntosViewModel 
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<AssuntoSimplesViewModel> Assuntos { get; set; } = new();
}

public class AssuntoSimplesViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

public class CriarPlanoEstudoItemViewModel
{
    public Guid AssuntoId { get; set; }
    public DateTime DataPrevista { get; set; }
}

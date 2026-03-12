using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Models.Auth;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-mail obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha obrigatória")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;
}

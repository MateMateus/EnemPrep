using System.ComponentModel.DataAnnotations;

namespace EnemPrep.Web.Models.Auth;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Nome obrigatório")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-mail obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha obrigatória")]
    [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
    [DataType(DataType.Password)]
    public string Senha { get; set; } = string.Empty;

    [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem")]
    [DataType(DataType.Password)]
    public string ConfirmarSenha { get; set; } = string.Empty;
}

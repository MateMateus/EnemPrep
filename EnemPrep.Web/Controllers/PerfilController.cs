using EnemPrep.Web.ApiClients;
using EnemPrep.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EnemPrep.Web.Controllers;

[Route("perfil")]
[VerificaSessaoAluno]
public class PerfilController : Controller
{
    private readonly IPerfilApiClient _perfilClient;
    private readonly ILogger<PerfilController> _logger;

    public PerfilController(IPerfilApiClient perfilClient, ILogger<PerfilController> logger)
    {
        _perfilClient = perfilClient;
        _logger = logger;
    }

    private Guid? GetUsuarioId()
    {
        var idStr = HttpContext.Session.GetString("UsuarioId");
        if (Guid.TryParse(idStr, out var id)) return id;
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        try
        {
            var perfil = await _perfilClient.GetPerfilAsync(usuarioId.Value, ct);
            if (perfil is null)
            {
                TempData["Erro"] = "Não foi possível carregar seu perfil.";
                return RedirectToAction("Index", "Home");
            }
            return View(perfil);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar perfil do usuário {UsuarioId}", usuarioId.Value);
            TempData["Erro"] = "Erro ao carregar seu perfil. Tente novamente.";
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost("nome")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AtualizarNome(string novoNome, CancellationToken ct)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        if (string.IsNullOrWhiteSpace(novoNome))
        {
            TempData["Erro"] = "O nome não pode ser vazio.";
            return RedirectToAction(nameof(Index));
        }

        var sucesso = await _perfilClient.AtualizarNomeAsync(usuarioId.Value, novoNome, ct);

        TempData[sucesso ? "Sucesso" : "Erro"] = sucesso
            ? "Nome atualizado com sucesso!"
            : "Erro ao atualizar o nome.";

        // Atualizar sessão com o novo nome
        if (sucesso) HttpContext.Session.SetString("NomeUsuario", novoNome);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("email")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AtualizarEmail(string novoEmail, CancellationToken ct)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        if (string.IsNullOrWhiteSpace(novoEmail))
        {
            TempData["Erro"] = "O email não pode ser vazio.";
            return RedirectToAction(nameof(Index));
        }

        var sucesso = await _perfilClient.AtualizarEmailAsync(usuarioId.Value, novoEmail, ct);

        TempData[sucesso ? "Sucesso" : "Erro"] = sucesso
            ? "Email atualizado com sucesso!"
            : "Erro ao atualizar email. O email pode já estar em uso.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("senha")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AtualizarSenha(string senhaAtual, string novaSenha, string confirmarSenha, CancellationToken ct)
    {
        var usuarioId = GetUsuarioId();
        if (!usuarioId.HasValue) return RedirectToAction("Login", "Auth");

        if (string.IsNullOrWhiteSpace(senhaAtual) || string.IsNullOrWhiteSpace(novaSenha))
        {
            TempData["Erro"] = "Preencha todos os campos de senha.";
            return RedirectToAction(nameof(Index));
        }

        if (novaSenha != confirmarSenha)
        {
            TempData["Erro"] = "A nova senha e a confirmação não coincidem.";
            return RedirectToAction(nameof(Index));
        }

        if (novaSenha.Length < 6)
        {
            TempData["Erro"] = "A nova senha deve ter pelo menos 6 caracteres.";
            return RedirectToAction(nameof(Index));
        }

        var (sucesso, errorMessage) = await _perfilClient.AtualizarSenhaAsync(usuarioId.Value, senhaAtual, novaSenha, ct);

        TempData[sucesso ? "Sucesso" : "Erro"] = sucesso
            ? "Senha alterada com sucesso!"
            : errorMessage ?? "Erro ao alterar senha.";

        return RedirectToAction(nameof(Index));
    }
}
